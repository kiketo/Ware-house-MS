﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> CreateProductAsync(string name, Unit unit, Category category, decimal buyPrice, double margin, string description, ApplicationUser user)
        {
            var newProduct = await this.context.Products.FirstOrDefaultAsync(t => t.Name == name);
            if (buyPrice < 0)
            {
                throw new ArgumentException($"Price cannot be negative number");
            }
            if (margin < 0)
            {
                throw new ArgumentException($"The price margin cannot be negative number");
            }
            decimal sellPrice = buyPrice * (100 + (decimal)margin) / 100;
            List<Warehouse> wareHouses = await this.context.Warehouses.ToListAsync();
            if (newProduct != null && newProduct.IsDeleted)
            {
                newProduct = new Product()
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
                    Description = description,
                    Creator = user
                };

                this.context.Attach(newProduct).State =
               Microsoft.EntityFrameworkCore
               .EntityState.Modified;
                await this.context.SaveChangesAsync();

                return newProduct;
            }

            newProduct = new Product()
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
                Description = description,
                Creator = user
            };

            await this.context.Products.AddAsync(newProduct);
            await this.context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            this.context.Attach(product).State =
                Microsoft.EntityFrameworkCore
                .EntityState.Modified;
            await this.context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await this.context.Products
                .Where(i => i.Id == productId && !i.IsDeleted)
               .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.OrderProductWarehouses)
                .Include(p => p.Unit)
                .Include(p => p.Warehouses)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ArgumentException($"Product does not exist!");
            }
            return product;
        }

        public async Task<ICollection<Product>> GetProductsByNameAsync(string name)
        {
            var product = await this.context.Products
                .Where(p => p.Name.Contains(name))
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.OrderProductWarehouses)
                .Include(p => p.Unit)
                .Include(p => p.Warehouses)
                .ToListAsync();

            if (product.Count == 0)
            {
                throw new ArgumentException($"Product `{name}` doesn't exist!");
            }
            return product;
        }

        public async Task<ICollection<Product>> GetProductsByCategoryAsync(Category category)
        {
            if (category == null || category.IsDeleted)
            {
                throw new ArgumentException("Category does not exists");
            }
            var productsByCategory = await this.context.Products
                .Where(p => p.Category == category && !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.OrderProductWarehouses)
                .Include(p => p.Unit)
                .Include(p => p.Warehouses)
                .ToListAsync();

            return productsByCategory;
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            var task = this.context.Products
                .Where(p => p.IsDeleted == false)
                .ToListAsync();

            return task;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var productToDelete = await this.context.Products
                .Where(u => u.Id == id)
                .Include(p => p.Creator)
                .FirstOrDefaultAsync();

            if (productToDelete == null || productToDelete.IsDeleted)
            {
                throw new ArgumentException($"Product with ID: `{id}` doesn't exist!");
            }
            productToDelete.ModifiedOn = DateTime.Now;
            productToDelete.IsDeleted = true;
            await this.context.SaveChangesAsync();
            return productToDelete;
        }

        public Task<List<Product>> GetProductsByCreatorId(string userId)
        {
            var products = this.context.Products
                .Where(p => p.CreatorId == userId && !p.IsDeleted)
                 .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Unit)
                .ToListAsync();

            return products;
        }

        //public async Task<Product> SetMarginAsync(int productId, double newMargin)
        //{
        //    var product = await this.context.Products.FirstOrDefaultAsync(i => i.Id == productId);
        //    if (product == null || product.IsDeleted)
        //    {
        //        throw new ArgumentException($"Product does not exist!");
        //    }
        //    if (newMargin < 0)
        //    {
        //        throw new ArgumentException($"The price margin cannot be negative number");
        //    }
        //    product.MarginInPercent = newMargin;
        //    product.ModifiedOn = DateTime.Now;
        //    await this.context.SaveChangesAsync();

        //    return product;
        //}

        //public async Task<Product> SetBuyPriceAsync(int productId, decimal price)
        //{
        //    var product = await this.context.Products.FirstOrDefaultAsync(i => i.Id == productId);
        //    if (product == null || product.IsDeleted)
        //    {
        //        throw new ArgumentException($"Product does not exist!");
        //    }
        //    if (price < 0)
        //    {
        //        throw new ArgumentException($"Price cannot be negative number");
        //    }
        //    product.ModifiedOn = DateTime.Now;
        //    product.BuyPrice = price;
        //    await this.context.SaveChangesAsync();

        //    return product;
        //}

        //public async Task<Product> ModifyProductNameAsync(string name, string newName)
        //{
        //    var productToMod = await this.context.Products.FirstOrDefaultAsync(t => t.Name == name);
        //    if (productToMod == null || productToMod.IsDeleted)
        //    {
        //        throw new ArgumentException($"Product {name} does not exists");
        //    }
        //    productToMod.Name = newName;
        //    productToMod.ModifiedOn = DateTime.Now;

        //    await this.context.SaveChangesAsync();
        //    return productToMod;
        //}

        //public async Task<Product> ModifyUnitAsync(Product product, Unit unit)
        //{
        //    product.ModifiedOn = DateTime.Now;
        //    product.Unit = unit;

        //    await this.context.SaveChangesAsync();
        //    return product;
        //}

        //public async Task<Product> ModifyCategoryAsync(Product product, Category category)
        //{
        //    product.ModifiedOn = DateTime.Now;
        //    product.Category = category;

        //    await this.context.SaveChangesAsync();
        //    return product;
        //}

        //public  Task<Product> GetProductByNameInclDeletedAsync(string name)
        //{
        //    var product =  this.context.Products
        //        .FirstOrDefaultAsync(u => u.Name == name);

        //    return product;
        //}

        //public async Task<Product> UndeleteProductAsync(string name)
        //{
        //    var product = await GetProductByNameInclDeletedAsync(name);

        //    product.IsDeleted = false;
        //    product.ModifiedOn = DateTime.Now;

        //    await this.context.SaveChangesAsync();

        //    return product;
        //}
    }
}
