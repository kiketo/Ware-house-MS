using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface ITownService
    {
        Task<Town> Add(string townToAddName);

        Task<Town> Edit(string oldTownName, string newTownName);

        Task<Town> Delete(string townToDeleteName);

        Task<Town> GetTown(string townToGetName);

        Task<IEnumerable<Town>> GetAllTowns();

        Task<Town> GetTownById(int id);
    }
}
