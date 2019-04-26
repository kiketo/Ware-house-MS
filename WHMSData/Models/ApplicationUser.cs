using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WHMSData.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Product> Products { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
