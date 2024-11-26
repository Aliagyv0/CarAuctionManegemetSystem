using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.Responses;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarAuctionApi.Service.Services;

public class TokenService(IConfiguration configuration,UserManager<User> userManager) : ITokenService
{
    public async Task<TokenResponse> GenerateToken(User user, int second)
    {
        var TokenResponse = new TokenResponse();
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };
        
        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var expires = DateTime.Now.AddSeconds(second);
        //Claima Role elave edecik

        SecurityKey securityToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Key"]));

        SigningCredentials credentials = new(securityToken, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken jwtSecurityToken = new(
            issuer: configuration["JwtSetting:Issuer"],
            audience: configuration["JwtSetting:Audience"],
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler handler = new();
        TokenResponse.AccessToken = handler.WriteToken(jwtSecurityToken);
        TokenResponse.ExpiresAt = DateTime.Now.AddSeconds(second);
        TokenResponse.RefreshToken = GenerateRefreshToken();
        return TokenResponse;
    }

    private string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        generator.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}