using System.Text;
using CarAuctionApi.App.Configurations;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Data;
using CarAuctionApi.Data.Context;
using CarAuctionApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace CarAuctionApi.App;

public static class ServiceRegistration
{
    public static void RegisterAppServices(this IServiceCollection services, IConfiguration configuration,
        ConfigureHostBuilder hostBuilder)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSetting:Issuer"],
                ValidAudience = configuration["JwtSetting:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Key"]))
            };
        });

        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<CarAuctionDbContext>()
            .AddDefaultTokenProviders();

        hostBuilder.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
        );

        services.AddDataService(configuration);
        services.AddService();
        services.AddRouting(opt => opt.LowercaseUrls = true);
        
        services.AddRouting();
        services.AddSwaggerConfiguration();
        
    }
}