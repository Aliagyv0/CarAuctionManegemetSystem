namespace CarAuctionApi.Service.DTOs.Request.Auth;

public record VerifyResetPasswordTokenDto(string ResetToken,string UserId);