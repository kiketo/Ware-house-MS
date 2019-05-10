using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp2.Areas.SuperAdmin.Models;
using WHMSWebApp2.Mappers;

namespace WHMSWebApp2.Areas.SuperAdmin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IViewModelMapper<ApplicationUser, UserViewModel> userMapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(IApplicationUserService applicationUserService, IViewModelMapper<ApplicationUser, UserViewModel> userMapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.applicationUserService = applicationUserService ?? throw new ArgumentNullException(nameof(applicationUserService));
            this.userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [Area("SuperAdmin")]
        [Route("superadmin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Dashboard()
        {
            var users = await this.applicationUserService.GetAllUsersAsync();
            UserViewModel model = new UserViewModel();

            try
            {
                model.UsersList = (users)
                    .Select(this.userMapper.MapFrom)
                    .ToList();
                foreach (var user in model.UsersList)
                {
                    user.Roles = new List<string>();
                    user.Roles = await userManager.GetRolesAsync(user.ApplicationUser);
                }
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }
    }
}