using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface ICategoryService
    {
        Category CreateCategory(string name);

        Category ModifyCategoryName(string name);

        Category DeleteCategory(string name);

        Category FindByName(string name);
    }
}
