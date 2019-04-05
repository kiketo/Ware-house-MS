using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Services.Interfaces;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly WHMSContext context;

        public CategoryService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Category CreateCategory(string name)
        {
            if (this.context.Categories.Any(t => t.Name == name))
            {
                throw new ArgumentException($"Category {name} already exists");
            }

            var newCategory = new Category()
            {
                Name = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                //Products = new List<Product>()
            };
            this.context.Categories.Add(newCategory);
            this.context.SaveChanges();

            return newCategory;
        }
        public Category ModifyCategoryName(string name)
        {
            var categoryToMod = this.context.Categories.FirstOrDefault(t => t.Name == name);
            if (categoryToMod == null || categoryToMod.IsDeleted)
            {
                throw new ArgumentException($"Category {name} does not exists");
            }
            categoryToMod.Name = name;
            categoryToMod.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return categoryToMod;
        }
        public bool DeleteCategory(string name) //TODO
        {
            var categoryToDelete = this.context.Products
                .FirstOrDefault(u => u.Name == name);

            if (categoryToDelete == null || categoryToDelete.IsDeleted)
            {
                throw new ArgumentException($"Category {name} does not exist!");
            }
            categoryToDelete.ModifiedOn = DateTime.Now;
            categoryToDelete.IsDeleted = true;
            this.context.SaveChanges();
            return true;
        }
        //public void AddProductToCategory(int categoryId, Product product)
        //{
        //    var category = this.context.Categories.FirstOrDefault(i => i.Id == categoryId);
        //    category.Products.Add(product);
        //    category.ModifiedOn = DateTime.Now;
        //    this.context.Categories.Update(category);
        //    this.context.SaveChanges();
        //}
        //public List<Product> GetProductsInCategory(int categoryId) //TODO: move to productservice
        //{
        //    return this.context.Categories.FirstOrDefault(i => i.Id == categoryId).Products.ToList();
        //}
        public Category FindByName(string name)
        {
            return this.context.Categories
                .FirstOrDefault(u => u.Name == name);
        }


    }
}
