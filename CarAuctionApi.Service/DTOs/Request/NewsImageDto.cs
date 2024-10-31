using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.DTOs.Request
{
    public record NewsImageDto(string NewsId, IFormFile Image);
}
