using CarAuctionApi.Service.DTOs.Request.Identity;
using CarAuctionApi.Service.Responses;

namespace CarAuctionApi.Service.Services.Interfaces;

public interface IIdentityService
{
    Task<ApiResponse> Register(RegisterDto dto);
    Task<ApiResponse> Login(LoginDto dto,int second);
    Task<ApiResponse> Update(UpdateUserDto dto, string userId);
    Task<ApiResponse> Info(string userId);
}