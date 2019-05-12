using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(string name, Unit unit, Category category, decimal buyPrice, double margin, string description, ApplicationUser user);

        Task<Product> GetProductByIdAsync(int productId);

        Task<ICollection<Product>> GetProductsByNameAsync(string name);

        Task<ICollection<Product>> GetProductsByCategoryAsync(Category category);


        Task<List<Product>> GetAllProductsAsync();

        Task<Product> DeleteProductAsync(int id);

        Task<Product> UpdateAsync(Product product);

        Task<List<Product>> GetProductsByCreatorId(string userId);

        //Task<Product> UndeleteProductAsync(string name);

        //Task<Product> GetProductByNameInclDeletedAsync(string name);

        //Task<Product> SetMarginAsync(int productId, double newMargin);

        //Task<Product> SetBuyPriceAsync(int productId, decimal price);

        //Task<Product> ModifyProductNameAsync(string name, string newName);

        //Task<Product> ModifyUnitAsync(Product product, Unit unit);

        //Task<Product> ModifyCategoryAsync(Product product, Category category);
    }
}