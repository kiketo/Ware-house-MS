using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WHMS.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> CreateProductAsync(string name, Unit unit, Category category, decimal buyPrice, double margin, string description)
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
            List<Warehouse> wareHouses = await this.context.Warehouses.ToListAsync();
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

            await this.context.Products.AddAsync(newProduct);
            await this.context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> SetMarginAsync(int productId, double newMargin)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(i => i.Id == productId);
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
            await this.context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> SetBuyPriceAsync(int productId, decimal price)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(i => i.Id == productId);
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
            await this.context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> ModifyProductNameAsync(string name, string newName)
        {
            var productToMod = await this.context.Products.FirstOrDefaultAsync(t => t.Name == name);
            if (productToMod == null || productToMod.IsDeleted)
            {
                throw new ArgumentException($"Product {name} does not exists");
            }
            productToMod.Name = newName;
            productToMod.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return productToMod;
        }

        public async Task<Product> ModifyUnitAsync(Product product, Unit unit)
        {
            product.ModifiedOn = DateTime.Now;
            product.Unit = unit;

            await this.context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> ModifyCategoryAsync(Product product, Category category)
        {
            product.ModifiedOn = DateTime.Now;
            product.Category = category;

            await this.context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await this.context.Products
                .Where(i => i.Id == productId && !i.IsDeleted)
                .Include(p=>p.Category)
                .Include(p=>p.Unit)
                .FirstOrDefaultAsync();
            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException($"Product does not exist!");
            }
            return product;
        }

        public async Task<ICollection<Product>> GetProductsByNameAsync(string name)
        {
            var product = await this.context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(name))
                .Where(p => p.IsDeleted == false)
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
                .ToListAsync();
            return productsByCategory;
        }

        public async Task<Product> GetProductByNameInclDeletedAsync(string name)
        {
            var product = await this.context.Products
                .FirstOrDefaultAsync(u => u.Name == name);

            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var task= await this.context.Products.ToListAsync();

            return task;
        }

        public async Task<Product> UndeleteProductAsync(string name)
        {
            var product = await GetProductByNameInclDeletedAsync(name);

            product.IsDeleted = false;
            product.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(string name)
        {
            var productToDelete = await this.context.Products
                .FirstOrDefaultAsync(u => u.Name == name);

            if (productToDelete == null || productToDelete.IsDeleted)
            {
                throw new ArgumentException($"Product `{name}` doesn't exist!");
            }
            productToDelete.ModifiedOn = DateTime.Now;
            productToDelete.IsDeleted = true;
            await this.context.SaveChangesAsync();
            return productToDelete;
        }

    }
}
