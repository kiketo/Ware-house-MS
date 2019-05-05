using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface ICategoryService
    {
        Category CreateCategory(string name);

        Category ModifyCategoryName(string name);

        Category DeleteCategory(string name);

        Task<Category> GetCategoryByNameAsync(string name);

        Category FindByName(string name);

        Category FindByID(int id);

        IEnumerable<Category> GetAllCategories();
    }
}
