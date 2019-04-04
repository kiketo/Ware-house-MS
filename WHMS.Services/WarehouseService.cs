using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly WHMSContext context;

        public WarehouseService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Warehouse CreateWarehouse(string name)
        {
            if (this.context.Warehouses.Any(t => t.Name == name))
            {
                throw new ArgumentException($"Warehouse {name} already exists");
            }
            List<Product> products = this.context.Products.ToList();
            var newWarehouse = new Warehouse()
            {
                Name = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Products = products.Select(w => new ProductWarehouse { Warehouse = w }).ToList()
               
            };
            this.context.Warehouses.Add(newWarehouse);
            this.context.SaveChanges();

            return newWarehouse;
        }
        public Warehouse ModifyWarehouseName(string name)
        {
            var warehousetToMod = this.context.Warehouses.FirstOrDefault(t => t.Name == name);
            if (warehousetToMod == null || warehousetToMod.IsDeleted)
            {
                throw new ArgumentException($"Warehouse {name} does not exists");
            }
            warehousetToMod.Name = name;
            warehousetToMod.ModifiedOn = DateTime.Now;

            this.context.Warehouses.Update(warehousetToMod);
            this.context.SaveChanges();
            return warehousetToMod;
        }

        public bool DeleteWarehouse(string name)
        {
            var warehouseToDelete = this.context.Warehouses
                .FirstOrDefault(u => u.Name == name);

            if (warehouseToDelete == null || warehouseToDelete.IsDeleted)
            {
                throw new ArgumentException($"Warehouse {name} does not exists");
            }
            warehouseToDelete.ModifiedOn = DateTime.Now;
            warehouseToDelete.IsDeleted = true;
            this.context.Warehouses.Update(warehouseToDelete);
            this.context.SaveChanges();
            return true;
        }

        public Warehouse GetByName(string name)
        {
            return this.context.Warehouses
                .FirstOrDefault(u => u.Name == name);
        }
    }
}
