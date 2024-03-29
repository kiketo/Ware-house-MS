﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Services.Contracts;
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

        public Warehouse CreateWarehouse(string name, Address address)
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
                Products = products.Select(w => new ProductWarehouse { Product = w }).ToList(),
                Address = address

            };
            this.context.Warehouses.Add(newWarehouse);
            this.context.SaveChanges();

            return newWarehouse;
        }

        public Warehouse ModifyWarehouseName(string currentName,string newName)
        {
            var warehousetToMod = this.context.Warehouses.FirstOrDefault(t => t.Name == currentName);
            if (warehousetToMod == null || warehousetToMod.IsDeleted)
            {
                throw new ArgumentException($"Warehouse {currentName} does not exists");
            }
            warehousetToMod.Name = newName;
            warehousetToMod.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return warehousetToMod;
        }

        public Warehouse DeleteWarehouse(string name)
        {
            var warehouseToDelete = this.context.Warehouses
                .FirstOrDefault(u => u.Name == name);

            if (warehouseToDelete == null || warehouseToDelete.IsDeleted)
            {
                throw new ArgumentException($"Warehouse {name} does not exists");
            }
            warehouseToDelete.ModifiedOn = DateTime.Now;
            warehouseToDelete.IsDeleted = true;
            this.context.SaveChanges();
            return warehouseToDelete;
        }

        public Warehouse GetByName(string name)
        {
            var warehouse = this.context.Warehouses
                .FirstOrDefault(u => u.Name == name);
            if (warehouse == null || warehouse.IsDeleted)
            {
                throw new ArgumentException($"Warehouse {name} does not exists");
            }
            return warehouse;
        }
        public ICollection<Warehouse> GetAllWarehouses()
        {
            var whs = this.context.Warehouses.ToList();
            return whs;
        }

    }
}
