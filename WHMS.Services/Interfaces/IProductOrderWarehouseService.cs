using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface IProductOrderWarehouseService
    {
        ProductOrderWarehouse Add(int productId, int orderId, int warehouseId, int quantity);

        ProductOrderWarehouse EditQuantity(int productId, int orderId, int warehouseId, int newQuantity);

        void Delete(int productId, int orderId, int warehouseId);
    }
}
