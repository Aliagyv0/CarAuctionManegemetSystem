namespace CarAuctionApi.Service.DTOs.Request.Auth;

public record UpdatePasswordDto(string Token,string UserId,string NewPassword, string ConfirmPassword);

    
