using CarAuctionApi.Data.Filters;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Services.Interfaces
{
    public interface IGenericService<in T> where T : class
    {
        public Task<ApiResponse> CreateAsync(T dto);
        public Task<ApiResponse> UpdateAsync(string id, T dto);
        public Task<ApiResponse> DeleteAsync(string id);
        public Task<ApiResponse> GetAllAsync(RequestFilter filter);
        public Task<ApiResponse> GetAsync(string id);
    }
}
