using CarAuctionApi.Service.DTOs.Request.Auth;
using CarAuctionApi.Service.DTOs.Request.Identity;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionApi.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto dto)
    {
        var result = await service.ConfirmEmail(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var result = await service.ResetPassword(dto);
        return StatusCode(result.StatusCode);
    }
    [HttpPost("verify-reset-token")]
    public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetPasswordTokenDto dto)
    {
        var result = await service.VerifyResetPasswordToken(dto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
    {
        var result = await service.UpdatePasswordAsync(dto);
        return StatusCode(result.StatusCode,result);
    }
    [HttpPost("refresh-token-login")]
    public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginDto dto)
    {
        var result = await service.RefreshTokenLoginAsync(dto, 60 * 60 * 12);
        return StatusCode(result.StatusCode, result);
    }
}