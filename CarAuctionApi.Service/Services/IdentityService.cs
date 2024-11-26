using AutoMapper;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.DTOs.Request.Identity;
using CarAuctionApi.Service.DTOs.Response;
using CarAuctionApi.Service.Exceptions;
using CarAuctionApi.Service.Responses;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CarAuctionApi.Service.Services;

public class IdentityService(
    UserManager<User> userManager,
    IHttpContextAccessor contextAccessor,
    IMailService mailService,
    SignInManager<User> signInManager,
    ITokenService tokenService,
    IMapper mapper,
    IAuthService authService) : IIdentityService
{
    public async Task<ApiResponse> Register(RegisterDto dto)
    {
        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Email = dto.Email,
            UserName = dto.UserName,
            FullName = dto.Name + " " + dto.Surname,
            SerialNumber = dto.SerialNumber,
            FinCode = dto.FinCode
        };
        IdentityResult result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return new ApiResponse()
            {
                StatusCode = 400,
                Items = result.Errors
            };
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var request = contextAccessor.HttpContext?.Request;

        string confirmationLink =
            $"{request?.Scheme}://{request?.Host}/api/auth/confirm-email?token={token}&userId={user.Id}";
        await mailService.SendMailAsync(user.Email, "Confirm your email",
            "Please confirm your account by clicking this link :" + confirmationLink);

        return new ApiResponse()
        {
            StatusCode = 200,
            Message = "Successfully registered!",
        };
    }

    public async Task<ApiResponse> Login(LoginDto dto, int second)
    {
        var user = await userManager.FindByNameAsync(dto.UsernameOrEmail);

        if (user == null)
        {
            user = await userManager.FindByEmailAsync(dto.UsernameOrEmail);

            if (user == null)
                throw new UserNotFoundException("Invalid username or password");
        }

        SignInResult result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, true);

        if (!result.Succeeded)
            throw new UserNotFoundException("Invalid username or password");

        var tokenResponse = await tokenService.GenerateToken(user, second);

        await authService.UpdateUserRefreshToken(user, tokenResponse.RefreshToken, tokenResponse.ExpiresAt.AddHours(2));
        
        return new ApiResponse()
        {
            StatusCode = 200,
            Items = tokenResponse
        };
    }

    public async Task<ApiResponse> Update(UpdateUserDto dto, string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException("User is not found");

        user.FinCode = dto.FinCode;
        user.SerialNumber = dto.SerialNumber;
        user.FullName = string.Join(" ", dto.Name, dto.Surname);
        user.Email = dto.Email;
        user.UserName = dto.UserName;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user, token, dto.Password);
        }

        await userManager.UpdateAsync(user);
        return new ApiResponse()
        {
            StatusCode = 200,
            Message = "Successfully updated!",
        };
    }

    public async Task<ApiResponse> Info(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException("User is not found");

        var data = mapper.Map<UserInfoDto>(user);

        return new ApiResponse()
        {
            StatusCode = 200,
            Items = data
        };
    }
}