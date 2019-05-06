using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IUnitService
    {
        Task<Unit> CreateUnitAsync(string name);
        Task<Unit> DeleteUnitNameAsync(int unitId);
        Task<List<Unit>> GetAllUnitsAsync();
        Task<Unit> ModifyUnitNameAsync(string name);
        Task<Unit> GetUnitAsync(string name);
        Task<Unit> GetUnitByIDAsync(int id);
    }
}