using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Responses
{
    public class ApiResponse
    {
        [JsonIgnore]
        public int StatusCode { get; set; }
        public Object Items { get; set; }
    }
}
