using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IOrderProductWarehouseService
    {
        Task<ICollection<OrderProductWarehouse>> GetProductsByOrderIdNWarehouseIdAsync(int orderId, int warehouseId);

        Task<ICollection<OrderProductWarehouse>> GetProductsByOrderIdWhereWantedQuantityIsOverZeroAsync(int orderId);
    }
}
