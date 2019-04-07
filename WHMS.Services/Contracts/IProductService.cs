using System.Collections.Generic;
using System.Linq;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IProductService
    {
        Product CreateProduct(string name, Unit unit, Category category, decimal buyPrice, double margin, string description);
        Product ModifyProductName(string name);
        Product DeleteProduct(string name);
        Product FindByName(string name);
        Product SetBuyPrice(int productId, decimal price);
        Product SetMargin(int productId, double newMargin);
        Product SetSellPrice(int productId, decimal price);
        Product GetProduct(string name);
        ICollection<Product> ProductsByCategory(Category category);
    }
}