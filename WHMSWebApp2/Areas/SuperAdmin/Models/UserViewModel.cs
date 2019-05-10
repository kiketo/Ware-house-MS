using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp2.Areas.SuperAdmin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }

        //public ICollection<string> RolesList { get; set; }

        public IReadOnlyCollection<UserViewModel> UsersList { get; set; }

    }
}

