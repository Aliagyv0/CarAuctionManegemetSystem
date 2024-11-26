namespace CarAuctionApi.Service.DTOs.Request.Auth;


    
public record ConfirmEmailDto(string token, string userId);