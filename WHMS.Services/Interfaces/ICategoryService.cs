using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddProductToCategory(int categoryId, Product product);
        Category CreateCategory(string name);
        bool DeleteCategory(string name);
        Category FindByName(string name);
        List<Product> GetProductsInCategory(int categoryId);
        Category ModifyCategoryName(string name);
    }
}
