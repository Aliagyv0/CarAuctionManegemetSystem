using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.Responses;

namespace CarAuctionApi.Service.Services.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> GenerateToken(User user,int second);

}