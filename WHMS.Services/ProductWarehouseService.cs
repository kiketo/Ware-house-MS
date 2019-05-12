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
    public class ProductWarehouseService : IProductWarehouseService
    {
        private readonly ApplicationDbContext context;

        public ProductWarehouseService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductWarehouse> AddQuantityAsync(int productId, int warehouseId, int quantity)
        {
            var pairPW = await this.context.ProductWarehouse
                .Where(p => p.ProductId == productId)
                .Where(w => w.WarehouseId == warehouseId)
                .ToAsyncEnumerable()
                .FirstOrDefault();

            if (pairPW == null)
            {
                throw new ArgumentException($"Product and/or warehouse does not exist");
            }
            pairPW.Quantity += quantity;

            await this.context.SaveChangesAsync();

            return pairPW;
        }

        public async Task<ProductWarehouse> SubstractQuantityAsync(int productId, int warehouseId, int quantity)
        {
            var pairPW = await this.context.ProductWarehouse
                .Where(p => p.ProductId == productId)
                .Where(w => w.WarehouseId == warehouseId)
                .ToAsyncEnumerable()
                .FirstOrDefault(); 

            if (pairPW == null)
            {
                throw new ArgumentException($"Product and/or warehouse does not exist");
            }

            if (quantity > pairPW.Quantity)
            {
                throw new ArgumentException($"In warehouse {pairPW.Warehouse} the quantity of product {pairPW.Product} is less than the needed amount");
            }

            pairPW.Quantity -= quantity;

            await this.context.SaveChangesAsync();

            return pairPW;
        }

        public async Task<int> GetQuantityAsync(int productId, int warehouseId)
        {
            var pairPW = await this.context.ProductWarehouse
                .Where(p => p.ProductId == productId)
                .Where(w => w.WarehouseId == warehouseId)
                .ToAsyncEnumerable()
                .FirstOrDefault(); 

            if (pairPW == null)
            {
                throw new ArgumentException($"Product or warehouse does not exist");
            }
            return pairPW.Quantity;
        }

        public Task<List<ProductWarehouse>> GetAllProductsInWarehouseAsync(int warehouseId)
        {
            Task<List<ProductWarehouse>> task = this.context.ProductWarehouse
                                                    .Include(pw => pw.Product)
                                                    .Include(pw => pw.Warehouse)
                                                    .Where(w => w.WarehouseId == warehouseId)
                                                    .ToListAsync();
            return task;
        }
        public async Task<ProductWarehouse> FindPairProductWarehouse(int warehouseId, int productId)
        {

            var pair = await this.context.ProductWarehouse.Where(p => p.ProductId == productId).FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);

            return pair;
        }

        public async Task<ProductWarehouse> UpdateAsync(ProductWarehouse pw)
        {
            this.context.Attach(pw).State =
                Microsoft.EntityFrameworkCore
                .EntityState.Modified;
            await this.context.SaveChangesAsync();
            return pw;
        }

        public Task<ProductWarehouse> GetByIdPair(int productId, int warehouseId)
        {
            var pw = this.context.ProductWarehouse
                .Where(p => p.ProductId == productId && p.WarehouseId == warehouseId)
                .Include(p => p.Product)
                .Include(p => p.Warehouse)
                .FirstOrDefaultAsync();
            return pw;
        }
    }
}
