using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarAuctionApi.Core.Models;

public class User : IdentityUser
{
 public string FullName { get; set; }   
 [MinLength(7)]
 [MaxLength(7)]
 public string FinCode { get; set; } 
 [MinLength(9)]
 [MaxLength(10)]
 public string SerialNumber { get; set; }
 public string? RefreshToken { get; set; }
 public DateTime? RefreshTokenExpiry { get; set; }
}