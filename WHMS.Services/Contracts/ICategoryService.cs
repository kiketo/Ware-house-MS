using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(string name);

        Task<Category> ModifyCategoryNameAsync(string name);

        Task<Category> DeleteCategoryAsync(string name);

        Task<Category> GetCategoryByNameAsync(string name);

        Task<Category> FindByNameAsync(string name);

        Task<Category> FindByIDAsync(int id);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
