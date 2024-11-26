using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.DTOs.Request.Auth;
using CarAuctionApi.Service.Responses;

namespace CarAuctionApi.Service.Services.Interfaces;

public interface IAuthService
{
        Task<ApiResponse> ConfirmEmail(ConfirmEmailDto dto);
        Task<ApiResponse> ResetPassword(ResetPasswordDto dto);
        Task<ApiResponse> UpdatePasswordAsync(UpdatePasswordDto dto);
        Task UpdateUserRefreshToken(User user, string refreshToken, DateTime refreshTokenExpiry);
        Task<ApiResponse> RefreshTokenLoginAsync(RefreshTokenLoginDto dto, int seconds);
        Task<ApiResponse> VerifyResetPasswordToken (VerifyResetPasswordTokenDto dto);
}