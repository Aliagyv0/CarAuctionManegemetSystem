using CarAuctionApi.Service.Profiles;
using CarAuctionApi.Service.Services;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service
{
    public static class ServiceRegistration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<ISliderServices, SliderService>();
            services.AddAutoMapper(typeof(SliderProfile));
            services.AddHttpContextAccessor();

            
        }
    }
}
