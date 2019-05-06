using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Category> CreateCategoryAsync(string name)
        {
            if (await this.context.Categories.AnyAsync(t => t.Name == name))
            {
                throw new ArgumentException($"Category {name} already exists");
            }

            var newCategory = new Category()
            {
                Name = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Products = new List<Product>()
            };
            await this.context.Categories.AddAsync(newCategory);
            await this.context.SaveChangesAsync();

            return newCategory;
        }

        public async Task<Category> ModifyCategoryNameAsync(string name)
        {
            var categoryToMod = await this.context.Categories.FirstOrDefaultAsync(t => t.Name == name);
            if (categoryToMod == null || categoryToMod.IsDeleted)
            {
                throw new ArgumentException($"Category {name} does not exists");
            }
            categoryToMod.Name = name;
            categoryToMod.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return categoryToMod;
        }

        public async Task<Category> DeleteCategoryAsync(string name)
        {
            var categoryToDelete = await this.context.Categories
                .FirstOrDefaultAsync(u => u.Name == name);

            if (categoryToDelete == null || categoryToDelete.IsDeleted)
            {
                throw new ArgumentException($"Category {name} does not exist!");
            }
            categoryToDelete.ModifiedOn = DateTime.Now;
            categoryToDelete.IsDeleted = true;
            await this.context.SaveChangesAsync();
            return categoryToDelete;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var category = await this.context.Categories
                        .Where(c => c.Name == name)
                        .FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            var category = await this.context.Categories
                .FirstOrDefaultAsync(u => u.Name == name);
            if (category == null || category.IsDeleted)
            {
                throw new ArgumentException($"Category {name} does not exist!");
            }
            return category;
        }

        public async Task<Category> FindByIDAsync(int id)
        {
            var category = await this.context.Categories
                .FirstOrDefaultAsync(u => u.Id == id);
            if (category == null || category.IsDeleted)
            {
                throw new ArgumentException($"Category does not exist!");
            }
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await this.context.Categories.ToListAsync();
        }
    }
}
