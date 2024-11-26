using CarAuctionApi.Data.Context;
using CarAuctionApi.Data.Repository;
using CarAuctionApi.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CarAuctionApi.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataService(this IServiceCollection services , IConfiguration configuration)
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
