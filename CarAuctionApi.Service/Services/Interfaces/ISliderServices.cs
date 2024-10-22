using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Services.Interfaces
{
    public interface ISliderServices
    {
        public Task<ApiResponse> CreateAsync(SliderDto dto);
        public Task<ApiResponse> UpdateAsync(string id, SliderDto dto);
        public Task<ApiResponse> DeleteAsync(string id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> GetAsync(string id);

    }
}
