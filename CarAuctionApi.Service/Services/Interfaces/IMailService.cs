namespace CarAuctionApi.Service.Services.Interfaces;

public interface IMailService
{
    Task SendMailAsync(string to, string subject, string body);
}