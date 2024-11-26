using CarAuctionApi.App.Middlewares;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.Helpers;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace CarAuctionApi.App;

public static class AppRun
{
    public static async Task RunApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.MapControllers();
        app.HandleException();

        #region DbInitializer

        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

        using (var scope = scopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            
            await DbInitializer.SeedAsync(userManager, roleManager);
        }

        #endregion

        app.Run();
    }
}