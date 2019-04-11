using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Deleting
{
    public class DeleteCategoryCommand : ICommand
    {
        ICategoryService categoryService;
        IProductService productService;
        public DeleteCategoryCommand(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }
        public string Execute(IReadOnlyList<string> parameters)
        {
            var categoryNameToDelete = parameters[0];
            var category = this.categoryService.FindByName(categoryNameToDelete);
            if (this.productService.ProductsByCategory(category).Count > 0)
            {
                throw new ArgumentException($"There are products in {categoryNameToDelete}\n\rCAtegory cannot be deleted");
            }
            this.categoryService.DeleteCategory(categoryNameToDelete);
            return $"Category {categoryNameToDelete} was deleted";

        }
    }
}
