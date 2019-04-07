using System;
using System.Collections.Generic;
using System.Text;
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

        public string Execute(IReadOnlyList<string> parameters)
        {
            var name = parameters[0];
            var category = this.categoryService.CreateCategory(name);

            return $"Category {category.Name} was created.";
        }
    }
}
