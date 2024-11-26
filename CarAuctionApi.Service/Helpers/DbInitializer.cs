using CarAuctionApi.Core.Enums;
using CarAuctionApi.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace CarAuctionApi.Service.Helpers;

public  class DbInitializer
{
    public static async Task SeedAsync(UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
    {
        #region Role Seed

        foreach (var role in Enum.GetValues(typeof(UserRole)))
        {
            if (!await roleManager.RoleExistsAsync(role.ToString()!))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = role.ToString()!
                });
            }
        }

        #endregion
        
        #region User Seed

        if (await userManager.FindByNameAsync("Admin") is null)
        {
            var admin = new User()
            {
                UserName = "Admin",
                Email = "admin@auction.com",
                EmailConfirmed = true,
                FullName = "Admin",
                FinCode = "1234567",
                SerialNumber = "AA1234567",
            };
            var result = await userManager.CreateAsync(admin, "Admin123.");

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            await userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
        }
        if (await userManager.FindByNameAsync("SuperAdmin") is null)
        {
            var sAdmin = new User()
            {
                UserName = "SuperAdmin",
                Email = "superadmin@auction.com",
                EmailConfirmed = true,
                FullName = "SuperAdmin",
                FinCode = "7654321",
                SerialNumber = "AA7654321",
            };
            
            var result = await userManager.CreateAsync(sAdmin, "Admin123.");

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            await userManager.AddToRolesAsync(sAdmin, [UserRole.SuperAdmin.ToString(), UserRole.Admin.ToString()]);
        }
        #endregion
    }
}