namespace CarAuctionApi.Service.DTOs.Request.Identity;

public record UpdateUserDto(string Name,string Surname,string FinCode,string SerialNumber,string Email,string UserName,string? Password,string? ConfirmPassword);