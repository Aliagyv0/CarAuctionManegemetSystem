using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Helpers
{
    public static class HttpContextHelper
    {
        public static string GetImageUrl(this HttpContext httpContext,string path)
        {
            if(httpContext != null && !string.IsNullOrWhiteSpace(path))
            {
                var scheme = httpContext.Request.Scheme;
                var host = httpContext.Request.Host.Value;

                return $"{scheme}://{host}{path}";
            }
            return null;
        }


    }
}
