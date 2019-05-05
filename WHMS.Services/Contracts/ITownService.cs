using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface ITownService
    {
        Task<Town> AddAsync(string townToAddName);

        Task<Town> EditAsync(string oldTownName, string newTownName);

        Task<Town> DeleteAsync(string townToDeleteName);

        Task<Town> GetTownAsync(string townToGetName);

        Task<IEnumerable<Town>> GetAllTownsAsync();

        Task<Town> GetTownByIdAsync(int id);
    }
}
