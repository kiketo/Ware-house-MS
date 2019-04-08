using System.Collections.Generic;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IWarehouseService
    {
        Warehouse CreateWarehouse(string name, Address address);

        Warehouse ModifyWarehouseName(string currentName, string newName);

        Warehouse DeleteWarehouse(string name);

        Warehouse GetByName(string name);

        ICollection<Warehouse> GetAllWarehouses();
    }
}