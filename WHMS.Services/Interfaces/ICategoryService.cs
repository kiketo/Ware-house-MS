using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface ICategoryService
    {
        //void AddProductToCategory(int categoryId, Product product);
        Category CreateCategory(string name);
        Category DeleteCategory(string name);
        Category FindByName(string name);
       // List<Product> GetProductsInCategory(int categoryId);
        Category ModifyCategoryName(string name);
    }
}
