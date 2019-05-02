using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IProductWarehouseService
    {
        Task<ProductWarehouse> AddQuantityAsync(int productId, int warehouseId, int quantity);
        Task<List<ProductWarehouse>> GetAllProductsInWarehouseWithQuantityOverZeroAsync(int warehouseId);
        Task<int> GetQuantityAsync(int productId, int warehouseId);
        Task<ProductWarehouse> SubstractQuantityAsync(int productId, int warehouseId, int quantity);

    }
}