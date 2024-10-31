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
    public interface INewsService : IGenericService<NewsDto>
    {
        Task<ApiResponse> DeleteImage(string id);
        Task<ApiResponse> ChangeIsMain(string id);
        Task<ApiResponse> AddImage(NewsImageDto dto);

    }
}
