using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreateCategoryCommand :ICommand
    {
        ICategoryService categoryService;
        public CreateCategoryCommand(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public Task<string> Execute(IReadOnlyList<string> parameters)
        {
            //var name = parameters[0];
            //var category = await this.categoryService.CreateCategory(name);

            //return $"Category {category.Name} was created.";
            throw new NotImplementedException();//TODO
        }
    }
}
