using WHMSData.Models;
using WHMSWebApp2.Mappers;

namespace WHMSWebApp2.Areas.SuperAdmin.Models
{
    public class UserViewModelMapper : IViewModelMapper<ApplicationUser, UserViewModel>
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;

        //public UserViewModelMapper(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        //{
        //    _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        //    _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        //}

        public UserViewModel MapFrom(ApplicationUser entity)

        => new UserViewModel
        {
            Id = entity.Id,
            UserName = entity.UserName,
            ApplicationUser=entity,
            //RolesList = await this._userManager.GetRolesAsync(entity),
            Email = entity.Email
        };
    }
}
