using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;
using Microsoft.EntityFrameworkCore;

namespace WHMS.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Product CreateProduct(string name, Unit unit, Category category, decimal buyPrice, double margin, string description)
        {
            if (this.context.Products.Any(t => t.Name == name))
            {
                throw new ArgumentException($"Product {name} already exists");
            }
            if (buyPrice <0)
            {
                throw new ArgumentException($"Price cannot be negative number");
            }
            if (margin<0)
            {
                throw new ArgumentException($"The price margin cannot be negative number");
            }
            decimal sellPrice = buyPrice*(100+(decimal)margin)/100;
            List<Warehouse> wareHouses = this.context.Warehouses.ToList();
            var newProduct = new Product()
            {
                Name = name,
                Unit = unit,
                Category = category,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                BuyPrice = buyPrice,
                MarginInPercent = margin,
                SellPrice = sellPrice,
                Warehouses = wareHouses.Select(w => new ProductWarehouse { Warehouse = w }).ToList(),
                Description = description
            };

            this.context.Products.Add(newProduct);
            this.context.SaveChanges();

            return newProduct;
        }
        public Product ModifyProductName(string name, string newName)
        {
            var productToMod = this.context.Products.FirstOrDefault(t => t.Name == name);
            if (productToMod == null || productToMod.IsDeleted)
            {
                throw new ArgumentException($"Product {name} does not exists");
            }
            productToMod.Name = newName;
            productToMod.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return productToMod;
        }
        public Product DeleteProduct(string name)
        {
            var productToDelete = this.context.Products
                .FirstOrDefault(u => u.Name == name);

            if (productToDelete == null || productToDelete.IsDeleted)
            {
                throw new ArgumentException($"Product `{name}` doesn't exist!");
            }
            productToDelete.ModifiedOn = DateTime.Now;
            productToDelete.IsDeleted = true;
            this.context.SaveChanges();
            return productToDelete;
        }

        public Product FindByName(string name)
        {
            Product product = this.context.Products
                .Include(p=>p.Category)
                .FirstOrDefault(u => u.Name == name);
            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException($"Product `{name}` doesn't exist!");
            }
            return product;
        }
        public Product FindByNameInclncludingDeleted(string name)
        {
            var product = this.context.Products
                .FirstOrDefault(u => u.Name == name);
            
            return product;
        }
        public Product SetBuyPrice(int productId, decimal price)
        {
            var product = this.context.Products.Where(i => i.Id == productId).FirstOrDefault();
            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException($"Product does not exist!");
            }
            if (price < 0)
            {
                throw new ArgumentException($"Price cannot be negative number");
            }
            product.ModifiedOn = DateTime.Now;
            product.BuyPrice = price;
            this.context.SaveChanges();

            return product;
        }
        public Product SetMargin(int productId, double newMargin)
        {
            var product = this.context.Products.Where(i => i.Id == productId).FirstOrDefault();
            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException($"Product does not exist!");
            }
            if (newMargin < 0)
            {
                throw new ArgumentException($"The price margin cannot be negative number");
            }
            product.MarginInPercent = newMargin;
            product.ModifiedOn = DateTime.Now;
            this.context.SaveChanges();

            return product;
        }
       
        public ICollection<Product> ProductsByCategory(Category category)
        {
            if (category == null || category.IsDeleted)
            {
                throw new ArgumentException("Category does not exists");
            }
            var productsByCategory = this.context.Products.Where(p => p.Category == category).ToList();
            return productsByCategory;
        }
        public Product GetProductById(int productId)
        {
            var product = this.context.Products.Where(i => i.Id == productId).FirstOrDefault();
            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException($"Product does not exist!");
            }
            return product;
        }
        public Product UndeleteProduct(string name)
        {
            var product = FindByNameInclncludingDeleted(name);

            product.IsDeleted = false;
            product.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return product;
        }
        public Product ModifyUnit(Product product, Unit unit)
        {
            product.ModifiedOn = DateTime.Now;
            product.Unit = unit;

            this.context.SaveChanges();
            return product;
        }
        public Product ModifyCategory(Product product, Category category)
        {
            product.ModifiedOn = DateTime.Now;
            product.Category = category;

            this.context.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
           return this.context.Products.ToList();
        }
    }
}
