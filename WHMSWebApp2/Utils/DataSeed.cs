using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMSWebApp2.Utils
{
    public static class DataSeed
    {
        internal static async Task SeedDatabaseWithSuperAdminAsync(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Roles.Any(u => u.Name == "SuperAdmin"))
                {
                    return;
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await rolemanager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                await rolemanager.CreateAsync(new IdentityRole { Name = "Admin" });
                await rolemanager.CreateAsync(new IdentityRole { Name = "User" });

                var superAdminUser = new ApplicationUser { UserName = "SuperAdmin", Email = "super@admin.user" };
                await userManager.CreateAsync(superAdminUser, "Admin123!");
                await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            }
        }
        internal static async Task SeedDatabaseWithDefaultCategory(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Categories.Any(c => c.Name == "default"))
                {
                    return;
                }

                Category defaultCat = new Category
                {
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    Name = "default",
                    Products = new List<Product>()

                };

                await dbContext.Categories.AddAsync(defaultCat);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
