using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.DTOs.Request
{
    public record NewsDto(string Title, string CategoryId, string Text, string Thesis, ICollection<string> TagIds,
    ICollection<IFormFile>? Images);
}
