using AutoMapper;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Data.Filters;
using CarAuctionApi.Data.Repository.Interfaces;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.DTOs.Response;
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
    public class TagService : ITagService
    {
        private readonly IReadRepository<Tag> _categoryReadRepository;
        private readonly IWriteRepository<Tag> _categoryWriteRepository;
        private readonly IMapper _mapper;



        public TagService(IReadRepository<Tag> categoryReadRepository, IWriteRepository<Tag> categoryWriteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(TagDto dto)
        {
            Tag category = _mapper.Map<Tag>(dto);


            await _categoryWriteRepository.AddAsync(category);
            await _categoryWriteRepository.SaveChangesAsync();

            return new ApiResponse()
            {
                StatusCode = 201,
                Items = category
            };

        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            await _categoryWriteRepository.SoftDelete(id);
            await _categoryWriteRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };
        }

        public async Task<ApiResponse> GetAllAsync(RequestFilter filter)
        {
            var tags = await _categoryReadRepository.GetAll(x => !x.IsDeleted,filter).ToListAsync();


            var result = _mapper.Map<List<TagGetDto>>(tags);

            return new ApiResponse()
            {
                StatusCode = 200,
                Items = result
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            var category = await _categoryReadRepository.GetAsync((x => x.Id.ToString() == id && !x.IsDeleted));



            var result = _mapper.Map<TagGetDto>(category);

            return new ApiResponse()
            {
                StatusCode = 200,
                Items = result
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, TagDto dto)
        {
            Tag category = await _categoryReadRepository.GetAsync((x => x.Id.ToString() == id && !x.IsDeleted));


            _mapper.Map(dto, category);
            _categoryWriteRepository.Update(category);
            await _categoryWriteRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 200,
                Items = category
            };
        }
    }
}
