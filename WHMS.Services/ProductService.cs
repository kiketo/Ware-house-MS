using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class ProductService
    {
        private readonly WHMSContext context;

        public ProductService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Product CreateProduct(string name)
        {
            if (this.context.Products.Any(t => t.Name == name))
            {
                throw new ArgumentException($"Product {name} already exists");
            }

            var newProduct = new Product()
            {
                Name = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                BuyPrice = 0,
                MarginInPercent = 0,
                SellPrice = 0,
                Warehouses = new List<ProductWarehouse>(),
                OrdersAndWarehouses = new List<ProductOrderWarehouse>()
                //???TODO Warehouse, OrderProductWarehouse lists


            };
            this.context.Products.Add(newProduct);
            this.context.SaveChanges();

            return newProduct;
        }
        public Product ModifyProductName(string name)
        {
            var productToMod = this.context.Products.FirstOrDefault(t => t.Name == name);
            if (productToMod == null || productToMod.IsDeleted)
            {
                throw new ArgumentException($"Product {name} does not exists");
            }
            productToMod.Name = name;
            productToMod.ModifiedOn = DateTime.Now;

            this.context.Products.Update(productToMod);
            this.context.SaveChanges();
            return productToMod;
        }
        public bool DeleteProduct (string name)
        {
            var productToDelete = this.context.Products
                .FirstOrDefault(u => u.Name == name);

            if (productToDelete == null || productToDelete.IsDeleted)
            {
                throw new ArgumentException($"Town `{name}` doesn't exist!");
            }
            productToDelete.ModifiedOn = DateTime.Now;
            productToDelete.IsDeleted = true;
            this.context.Products.Update(productToDelete);
            this.context.SaveChanges();
            return true;
        }

        public Product FindByName(string name)
        {
            return this.context.Products
                .FirstOrDefault(u => u.Name == name);
        }

        //public IReadOnlyCollection<Product> GetProducts(int skip, int take)
        //{
        //    return this.context.Products
        //        .Skip(skip)
        //        .Take(take)
        //        .ToList();
        //}


    }
}
