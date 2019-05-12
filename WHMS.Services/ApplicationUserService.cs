using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync()
        {
             var users= this.context.Users
                .ToListAsync();

            return users;
        }

        public async Task<ApplicationUser> ChangeUserRoleAsync(string userId)
        {
            var updatedUser = await this.context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            var userRole =await this.userManager.GetRolesAsync(updatedUser);
            
            var allRoles = await this.context.Roles
                .Where(u => u.Name != "SuperAdmin")
                .ToListAsync();
            if (userRole[0]==allRoles[0].Name)
            {
                await this.userManager.RemoveFromRoleAsync(updatedUser, userRole[0]);
                await this.userManager.AddToRoleAsync(updatedUser, allRoles[1].Name);
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(updatedUser, userRole[0]);
                await this.userManager.AddToRoleAsync(updatedUser, allRoles[0].Name);
            }

            await this.context.SaveChangesAsync();

            return updatedUser;
        }

    }
}
