﻿using System.Collections.Generic;
using System.Linq;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IProductService
    {
        Product CreateProduct(string name, Unit unit, Category category, decimal buyPrice, double margin, string description);
        Product ModifyProductName(string name, string newName);
        Product DeleteProduct(string name);
        Product FindByName(string name);
        Product FindByNameInclncludingDeleted(string name);
        Product SetBuyPrice(int productId, decimal price);
        Product SetMargin(int productId, double newMargin);
        ICollection<Product> ProductsByCategory(Category category);
        Product GetProductById(int productId);
        Product UndeleteProduct(string name);
        Product ModifyUnit(Product product, Unit unit);
        Product ModifyCategory(Product product, Category category);

    }
}