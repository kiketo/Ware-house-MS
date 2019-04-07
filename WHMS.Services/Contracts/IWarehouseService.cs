using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IWarehouseService
    {
        Warehouse CreateWarehouse(string name, Address address);

        Warehouse ModifyWarehouseName(string name);

        Warehouse DeleteWarehouse(string name);

        Warehouse GetByName(string name);
    }
}