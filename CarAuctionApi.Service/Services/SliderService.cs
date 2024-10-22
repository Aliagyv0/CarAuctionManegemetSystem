using AutoMapper;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Data.Repository.Interfaces;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Responses;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SliderService(IReadRepository<Slider> sliderReadRepository, IWriteRepository<Slider> sliderWriteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _sliderReadRepository = sliderReadRepository;
            _sliderWriteRepository = sliderWriteRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<ApiResponse> CreateAsync(SliderDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateAsync(string id, SliderDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
