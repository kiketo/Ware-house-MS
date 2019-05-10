using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class OrderProductWarehouseService : IOrderProductWarehouseService
    {
        private readonly ApplicationDbContext context;

        public OrderProductWarehouseService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ICollection<OrderProductWarehouse>> GetProductsByOrderIdNWarehouseIdAsync(int orderId, int warehouseId)
        {
            var opw = await this.context.OrderProductWarehouses
                .Where(o => o.OrderId == orderId)
                .Where(w => w.WarehouseId == warehouseId)
                .ToListAsync();
            return opw;
        }

        public async Task<ICollection<OrderProductWarehouse>> GetProductsByOrderIdWhereWantedQuantityIsOverZeroAsync(int orderId)
        {
            var orderProducts = await this.context.OrderProductWarehouses
                .Where(o => o.OrderId == orderId)
                .Where(w => w.WantedQuantity > 0)
                .ToListAsync();
            return orderProducts;
        }

    }
}
