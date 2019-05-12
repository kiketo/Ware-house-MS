using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IWarehouseService
    {
        Task<Warehouse> CreateWarehouseAsync(string name, Address address);

        //Task<Warehouse> ModifyWarehouseNameAsync(string currentName, string newName);

        //Task<Warehouse> DeleteWarehouseAsync(string name);

        Task<Warehouse> GetByNameAsync(string name);

        Task<ICollection<Warehouse>> GetAllWarehousesAsync();

        Task<Warehouse> GetByIdAsync(int id);
        Task<List<Warehouse>> GetAllWarehousesAsync();
    }
}