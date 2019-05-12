using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IOrderProductWarehouseService
    {
        Task<ICollection<OrderProductWarehouse>> GetProductsByOrderIdWhereWantedQuantityIsOverZeroAsync(int orderId);

        Task<OrderProductWarehouse> UpdateWantedQuantity(OrderProductWarehouse opw);

        Task<OrderProductWarehouse> GetOPW(int productId, int warehouseId, int orderId);
    }
}
