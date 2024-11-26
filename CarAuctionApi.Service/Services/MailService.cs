using System.Net;
using System.Net.Mail;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CarAuctionApi.Service.Services;

public class MailService(IConfiguration configuration) : IMailService
{
    public async Task SendMailAsync(string to, string subject, string body)
    {
        MailMessage mailMessage = new();
        mailMessage.From = new MailAddress(configuration.GetValue<string>("MailSetting:Address"));
        mailMessage.To.Add(to);
        mailMessage.Subject = subject;
        mailMessage.Body = body;

        SmtpClient smtpClient = new();
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(configuration.GetValue<string>("MailSetting:Address"), configuration.GetValue<string>("MailSetting:Password"));
        smtpClient.EnableSsl = true;
        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Mail Error" + ex.Message);
        }
    }
}