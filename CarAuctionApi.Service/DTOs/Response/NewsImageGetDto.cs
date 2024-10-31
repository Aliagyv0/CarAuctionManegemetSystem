using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.DTOs.Response
{
    public class NewsImageGetDto()
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }
}
