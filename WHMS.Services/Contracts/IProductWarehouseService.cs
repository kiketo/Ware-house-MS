using System.Collections.Generic;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IProductWarehouseService
    {
        ProductWarehouse AddQuantity(int productId, int warehouseId, int quantity);
        ICollection<ProductWarehouse> GetAllProductsInWarehouse(int warehouseId);
        int GetQuantity(int productId, int warehouseId);
        ProductWarehouse SubstractQuantity(int productId, int warehouseId, int quantity);

    }
}