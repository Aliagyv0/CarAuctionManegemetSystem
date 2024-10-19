using CarAuctionApi.Data.Context;
using CarAuctionApi.Data.Repository;
using CarAuctionApi.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataService(this IServiceCollection services , IConfigurationManager configuration)
        {
            services.AddDbContext<CarAuctionDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Development"));
            });

            services.AddScoped(typeof(IReadRepository<>),typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        }
    }
}
