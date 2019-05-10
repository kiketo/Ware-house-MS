using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IApplicationUserService
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();


    }
}
