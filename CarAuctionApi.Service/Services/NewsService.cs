using AutoMapper;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Data.Filters;
using CarAuctionApi.Data.Repository.Interfaces;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.DTOs.Response;
using CarAuctionApi.Service.Exceptions;
using CarAuctionApi.Service.Helpers;
using CarAuctionApi.Service.Responses;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Services
{
    public class NewsService : INewsService
    {
        private readonly IWriteRepository<News> _writeRepository;
        private readonly IWriteRepository<NewsImage> _writeImageRepository;
        private readonly IWriteRepository<NewsTag> _writeTagsRepository;
        private readonly IReadRepository<News> _readRepository;
        private readonly IReadRepository<NewsTag> _readTagsRepository;
        private readonly IReadRepository<NewsImage> _readImageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsService(IWriteRepository<News> writeRepository, IWriteRepository<NewsImage> writeImageRepository, IWriteRepository<NewsTag> writeTagsRepository, IReadRepository<News> readRepository, IReadRepository<NewsTag> readTagsRepository, IReadRepository<NewsImage> readImageRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _writeRepository = writeRepository;
            _writeImageRepository = writeImageRepository;
            _writeTagsRepository = writeTagsRepository;
            _readRepository = readRepository;
            _readTagsRepository = readTagsRepository;
            _readImageRepository = readImageRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ApiResponse> CreateAsync(NewsDto dto)
        {
            int fileCount = 1;
            News news = _mapper.Map<News>(dto);

            foreach (var file in dto.Images)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "news");
                string fileName = file.SaveImage(path);

                NewsImage image = new NewsImage()
                {
                    ImageUrl = fileName,
                    News = news,
                    IsMain = fileCount == 1
                };
                await _writeImageRepository.AddAsync(image);
                fileCount++;
            }

            foreach (var tagId in dto.TagIds)
            {
                NewsTag newsTag = new()
                {
                    TagId = Guid.Parse(tagId),
                    News = news,
                };
                await _writeTagsRepository.AddAsync(newsTag);
            }

            await _writeRepository.AddAsync(news);
            await _writeRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 201,
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, NewsDto dto)
        {
            int fileCount = 1;
            var news = await _readRepository.GetAll(x => x.Id.ToString() == id && !x.IsDeleted,null)
                .Include(x => x.NewsImages)
                .Include(x => x.NewsTags).ThenInclude(x => x.Tag)
                .Include(x => x.Category).FirstOrDefaultAsync();

            if (news == null)
                return new ApiResponse()
                {
                    StatusCode = 404,
                    Message = "News is not found!"
                };


            _mapper.Map(dto, news);

            if (dto.Images is not null)
            {
                foreach (var file in dto.Images)
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "news");
                    string fileName = file.SaveImage(path);

                    NewsImage image = new NewsImage()
                    {
                        ImageUrl = fileName,
                        News = news,
                        IsMain = (news.NewsImages.Count == 0 && fileCount == 1),
                    };
                    await _writeImageRepository.AddAsync(image);
                    fileCount++;
                }
            }

            List<NewsTag> removeableTags = await _readTagsRepository.GetAll(x => !x.IsDeleted,null)
                .Where(x => !dto.TagIds.Contains(x.TagId.ToString()) && x.NewsID.ToString() == id)
                .ToListAsync();

            _writeTagsRepository.DeleteRange(removeableTags);

            foreach (var newTagId in dto.TagIds)
            {
                if (_readTagsRepository.GetAll(x => x.NewsID.ToString() == id && x.TagId.ToString() == newTagId,null).Count() > 0)
                    continue;

                NewsTag newsTag = new()
                {
                    TagId = Guid.Parse(newTagId),
                    News = news,
                };
                await _writeTagsRepository.AddAsync(newsTag);

            }

            _writeRepository.Update(news);
            await _writeRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };
        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            var news = await _readRepository.GetAsync(x => x.Id.ToString() == id && !x.IsDeleted);
            news.IsDeleted = true;
            _writeRepository.Update(news);
            await _writeRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };
        }

        public async Task<ApiResponse> GetAllAsync(RequestFilter filter)
        {
            var news = await _readRepository.GetAll(x => !x.IsDeleted, filter)
                .Include(x => x.NewsImages)
                .Include(x => x.NewsTags).ThenInclude(x => x.Tag)
                .Include(x => x.Category)
              .ToListAsync();

            var result = _mapper.Map<ICollection<NewsGetDto>>(news);

            return new ApiResponse()
            {
                StatusCode = 200,
                Items = result
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            var news = await _readRepository.GetAll(x => x.Id.ToString() == id && !x.IsDeleted,null)
                .Include(x => x.NewsImages)
                .Include(x => x.NewsTags).ThenInclude(x => x.Tag)
                .Include(x => x.Category)
                .FirstOrDefaultAsync();
            var result = _mapper.Map<News, NewsGetDto>(news);

            
            return new ApiResponse()
            {
                StatusCode = 200,
                Items = result
            };
        }

        public async Task<ApiResponse> DeleteImage(string id)
        {

            
            var image = await _readImageRepository.GetAsync(x => x.Id.ToString() == id && !x.IsDeleted);
            if (image.IsMain)
                throw new DeleteMainImageException("You cannot delete the main image");
            
            var path = _webHostEnvironment.WebRootPath+ $"/images/news/{image.ImageUrl}";
          var result =  FileHelper.RemoveImage(path);
            
            if(!result)
            {
                return new ApiResponse()
                {
                    StatusCode = 404,
                    Message="Image not Found"
                };

            }
           await _writeImageRepository.Delete(id);
           await _writeRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };
        }

        public async Task<ApiResponse> ChangeIsMain(string id)
        {
            var image = await _readImageRepository.GetAsync(x => x.Id.ToString() == id && !x.IsDeleted);

            var mainImage = await _readImageRepository.GetAsync(x => x.IsMain && !x.IsDeleted && x.NewsId == image.NewsId);

            if (image.IsMain)
                return new ApiResponse()
                {
                    StatusCode = 400,
                    Message = "You cannot change the main image",
                };
            mainImage.IsMain = false;
            image.IsMain = true;
            await _writeImageRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };
        }

        public async Task<ApiResponse> AddImage(NewsImageDto dto)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "news");
            string fileName = dto.Image.SaveImage(path);

            NewsImage image = new NewsImage()
            {
                ImageUrl = fileName,
                NewsId = Guid.Parse(dto.NewsId),
                IsMain = false,
            };
            await _writeImageRepository.AddAsync(image);
          await _writeRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 204
            };
            
        }
    }
}
