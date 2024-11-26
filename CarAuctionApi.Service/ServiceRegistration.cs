using CarAuctionApi.Service.Profiles;
using CarAuctionApi.Service.Services;
using CarAuctionApi.Service.Services.Interfaces;
using CarAuctionApi.Service.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace CarAuctionApi.Service
{
    public static class ServiceRegistration
    {        
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<ISliderServices, SliderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();  
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new BaseProfile(services.BuildServiceProvider().GetService<IHttpContextAccessor>()));
            },typeof(BaseProfile));
            services.AddHttpContextAccessor();

            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SliderDtoValidator>());
        }
    }
}
