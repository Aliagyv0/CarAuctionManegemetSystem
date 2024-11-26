using System.Security.Claims;
using CarAuctionApi.Service.DTOs.Request.Identity;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionApi.App.Controllers;
[ApiController]
[Route("api/[controller]")]
public class IdentityController(IIdentityService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await service.Register(dto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await service.Login(dto,60*60*12);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await service.Update(dto,userId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("info")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Info()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await service.Info(userId);
        return StatusCode(result.StatusCode, result);
    }

}