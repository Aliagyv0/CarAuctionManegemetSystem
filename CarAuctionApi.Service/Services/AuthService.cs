using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.DTOs.Request.Auth;
using CarAuctionApi.Service.Exceptions;
using CarAuctionApi.Service.Helpers;
using CarAuctionApi.Service.Responses;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionApi.Service.Services;

public class AuthService(UserManager<User> userManager, IMailService mailService, IHttpContextAccessor contextAccessor,ITokenService tokenService)
    : IAuthService
{
    public async Task<ApiResponse> ConfirmEmail(ConfirmEmailDto dto)
    {
        if (dto?.userId == null || dto?.token == null)
        {
            throw new TokenExpiredException();
        }

        var user = await userManager.FindByIdAsync(dto.userId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var token = dto.token.Replace(" ", "+");

        var result = await userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return new ApiResponse()
            {
                StatusCode = 200,
                Message = "Thank you for confirming your email!"
            };
        }

        return new ApiResponse()
        {
            StatusCode = 400,
            Message = "Email not confirmed!"
        };
    }

    public async Task<ApiResponse> ResetPassword(ResetPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return new ApiResponse()
            {
                StatusCode = 204
            };
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetToken = token.EncodeToken();

        var request = contextAccessor.HttpContext?.Request;

        string confirmationLink =
            $"{request?.Scheme}://{request?.Host}/api/auth/reset-password?token={resetToken}&userId={user.Id}";

        await mailService.SendMailAsync(user.Email, "Reset password",
            "Please reset your password by clicking this link :" + confirmationLink);

        return new ApiResponse()
        {
            StatusCode = 204,
        };
    }

    public async Task<ApiResponse> UpdatePasswordAsync(UpdatePasswordDto dto)
    {
        var user = await userManager.FindByIdAsync(dto.UserId);
        if (user is not null)
        {
            var decodedToken = dto.Token.DecodeToken();

            var result = await userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);

            if (result.Succeeded)
            {
                await userManager.UpdateSecurityStampAsync(user);
            }
        }

        return new ApiResponse()
        {
            StatusCode = 200,
            Message = "Thank you for changing your password!"
        };
    }

    public async Task<ApiResponse> RefreshTokenLoginAsync(RefreshTokenLoginDto dto ,int seconds)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x=>x.RefreshToken == dto.RefreshToken);
        if (user is null || user.RefreshTokenExpiry < DateTime.Now.AddMinutes(1)) 
        throw new TokenExpiredException();
        
        var tokenResponse = await tokenService.GenerateToken(user,seconds);
        await UpdateUserRefreshToken(user, tokenResponse.RefreshToken, tokenResponse.ExpiresAt.AddHours(2));
        
        return new ApiResponse()
        {
            StatusCode = 200,
            Items = tokenResponse
        };
    }

public async Task UpdateUserRefreshToken(User user, string refreshToken, DateTime refreshTokenExpiry)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = refreshTokenExpiry;
        await userManager.UpdateSecurityStampAsync(user);
    }

    public async Task<ApiResponse> VerifyResetPasswordToken(VerifyResetPasswordTokenDto dto)
    {
        var user = await userManager.FindByIdAsync(dto.UserId);
        var isValid = false;
        
        if (user is not null)
        {
            var decodedResetToken = dto.ResetToken.DecodeToken();
            isValid =  await userManager.VerifyUserTokenAsync(user,userManager.Options.Tokens.PasswordResetTokenProvider,"ResetPassword",decodedResetToken);
        }
        return new ApiResponse()
        {
            StatusCode = isValid ? 200 : 400,
            Message = isValid ? "Your token is valid" : "Your token is invalid"
        };
    }
        
}