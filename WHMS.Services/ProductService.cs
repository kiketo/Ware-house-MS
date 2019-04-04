using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Services.Interfaces;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class ProductService : IProductService
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
            //
            List<Warehouse> wareHouses = this.context.Warehouses.ToList();
            var newProduct = new Product()
            {
                Name = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                BuyPrice = 0,
                MarginInPercent = 0,
                SellPrice = 0,
                Warehouses = wareHouses.Select(w => new ProductWarehouse { Warehouse = w }).ToList()
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
        public Product DeleteProduct (string name)
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
            return productToDelete;
        }

        public Product FindByName(string name)
        {
            return this.context.Products
                .FirstOrDefault(u => u.Name == name);
        }
        public void SetBuyPrice(int productId, decimal price)
        {
            var product = this.context.Products.Where(i => i.Id == productId).FirstOrDefault();
            product.ModifiedOn = DateTime.Now;
            product.BuyPrice = price;
            this.context.Products.Update(product);
            this.context.SaveChanges();
        }
        public void SetMargin(int productId, double newMargin)
        {
            var product = this.context.Products.Where(i => i.Id == productId).FirstOrDefault();
            product.MarginInPercent = newMargin;
            product.ModifiedOn = DateTime.Now;
            this.context.Products.Update(product);
            this.context.SaveChanges();
        }
        public void SetSellPrice(Product product)
        {
            product.SellPrice = product.BuyPrice + product.BuyPrice * (decimal)product.MarginInPercent;
            product.ModifiedOn = DateTime.Now;
            this.context.Products.Update(product);
            this.context.SaveChanges();
        }
        public void SetSellPriceManually(int productId, decimal price)
        {
            var product = this.context.Products.Where(i => i.Id == productId).FirstOrDefault();
            product.BuyPrice = price;
            product.ModifiedOn = DateTime.Now;
            this.context.Products.Update(product);
            this.context.SaveChanges();
        }

    }
}
