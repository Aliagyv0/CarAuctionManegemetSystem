using System.ComponentModel.DataAnnotations;

namespace CarAuctionApi.Service.DTOs.Request.Identity;

public class RegisterDto
{
   public string Name { get; set; }
   public string Surname { get; set; }
   public string FinCode { get; set; }
   public string SerialNumber { get; set; }
   [DataType(DataType.EmailAddress)]
   public string Email { get; set; }
   public string UserName { get; set; }
   [DataType(DataType.Password)]
   public string Password { get; set; }
   [DataType(DataType.Password)]
   [Compare("Password")]
   public string ConfirmPassword { get; set; }
}

    
