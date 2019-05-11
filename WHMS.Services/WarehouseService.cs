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
    public class WarehouseService : IWarehouseService
    {
        private readonly ApplicationDbContext context;

        public WarehouseService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Warehouse> CreateWarehouseAsync(string name, Address address)
        {
            var newWarehouse = (await this.context.Warehouses.FirstOrDefaultAsync(t => t.Name == name));
            if (newWarehouse!=null)
            {
                return newWarehouse;
            }
            List<Product> products = await this.context.Products.ToListAsync();
            newWarehouse = new Warehouse()
            {
                Name = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Products = products.Select(w => new ProductWarehouse { Product = w }).ToList(),
                Address = address
            };
            await this.context.Warehouses.AddAsync(newWarehouse);
            await this.context.SaveChangesAsync();

            return newWarehouse;
        }

        public async Task<Warehouse> GetByNameAsync(string name)
        {
            var warehouse = await this.context.Warehouses
                .FirstOrDefaultAsync(u => u.Name == name);
            if (warehouse == null || warehouse.IsDeleted)
            {
                throw new ArgumentException($"Warehouse {name} does not exists");
            }
            return warehouse;
        }

        public Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            var whs = this.context.Warehouses.ToListAsync();
            return whs;
        }

        //public async Task<Warehouse> ModifyWarehouseNameAsync(string currentName,string newName)
        //{
        //    var warehousetToMod = await this.context.Warehouses.FirstOrDefaultAsync(t => t.Name == currentName);
        //    if (warehousetToMod == null || warehousetToMod.IsDeleted)
        //    {
        //        throw new ArgumentException($"Warehouse {currentName} does not exists");
        //    }
        //    warehousetToMod.Name = newName;
        //    warehousetToMod.ModifiedOn = DateTime.Now;

        //    await this.context.SaveChangesAsync();
        //    return warehousetToMod;
        //}

        //public async Task<Warehouse> DeleteWarehouseAsync(string name)
        //{
        //    var warehouseToDelete = await this.context.Warehouses
        //        .FirstOrDefaultAsync(u => u.Name == name);

        //    if (warehouseToDelete == null || warehouseToDelete.IsDeleted)
        //    {
        //        throw new ArgumentException($"Warehouse {name} does not exists");
        //    }
        //    warehouseToDelete.ModifiedOn = DateTime.Now;
        //    warehouseToDelete.IsDeleted = true;
        //    await this.context.SaveChangesAsync();
        //    return warehouseToDelete;
        //}
    }
}
