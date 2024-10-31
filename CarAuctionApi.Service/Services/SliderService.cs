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
    public class SliderService : ISliderServices
    {
        private readonly IReadRepository<Slider> _sliderReadRepository;
        private readonly IWriteRepository<Slider> _sliderWriteRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderService(IReadRepository<Slider> sliderReadRepository, IWriteRepository<Slider> sliderWriteRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _sliderReadRepository = sliderReadRepository;
            _sliderWriteRepository = sliderWriteRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ApiResponse> CreateAsync(SliderDto dto)
        {
            Slider slider = _mapper.Map<Slider>(dto);

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "sliders");

            slider.ImageUrl = dto.Image.SaveImage(path);
            await _sliderWriteRepository.AddAsync(slider);
            await _sliderWriteRepository.SaveChangesAsync();

            return new ApiResponse()
            {
                StatusCode = 201,
                Items = slider
            };

        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            await _sliderWriteRepository.SoftDelete(id);
            await _sliderWriteRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };
        }

        public async Task<ApiResponse> GetAllAsync(RequestFilter filter)
        {
            var sliders = await _sliderReadRepository.GetAll(x=>!x.IsDeleted, filter).ToListAsync();

          
            var result = _mapper.Map<SliderGetDto>(sliders);

            return new ApiResponse()
            {
                StatusCode = 200,
                Items = result
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            var slider = await _sliderReadRepository.GetAsync((x => x.Id.ToString() == id && !x.IsDeleted));

            var result = _mapper.Map<SliderGetDto>(slider);

            return new ApiResponse()
            {
                StatusCode = 200,
                Items = result
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, SliderDto dto)
        {
            Slider slider = await _sliderReadRepository.GetAsync((x=>x.Id.ToString() == id&& !x.IsDeleted));

            if (dto.Image != null)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "sliders");
                FileHelper.RemoveImage(Path.Combine(path,slider.ImageUrl));

                slider.ImageUrl =dto.Image.SaveImage(path);
            }
            _mapper.Map(dto,slider);
            _sliderWriteRepository.Update(slider);
            await _sliderWriteRepository.SaveChangesAsync();
            return new ApiResponse()
            {
                StatusCode = 200,
                Items = slider
            };
        }
    }
}
