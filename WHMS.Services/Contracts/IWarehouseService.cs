using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IWarehouseService
    {
        Warehouse CreateWarehouse(string name);

        Warehouse ModifyWarehouseName(string name);

        Warehouse DeleteWarehouse(string name);

        Warehouse GetByName(string name);
    }
}