using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IProductService
    {
        Product CreateProduct(string name, Unit unit, Category category, decimal buyPrice, double margin, string description);
        Product SetMargin(int productId, double newMargin);
        Product SetBuyPrice(int productId, decimal price);
        Product ModifyProductName(string name, string newName);
        Product ModifyUnit(Product product, Unit unit);
        Product ModifyCategory(Product product, Category category);
        Task<Product> GetProductByIdAsync(int productId);
        Task<ICollection<Product>> GetProductsByNameAsync(string name);
        Task<ICollection<Product>> GetProductsByCategoryAsync(Category category);
        Product GetProductByNameInclDeleted(string name);
        Task<List<Product>> GetAllProductsAsync();
        Product UndeleteProduct(string name);
        Product DeleteProduct(string name);

    }
}