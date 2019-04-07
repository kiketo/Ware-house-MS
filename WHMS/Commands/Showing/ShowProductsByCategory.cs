using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Showing
{
    public class ShowProductsByCategory : ICommand
    {
        IProductService productService;
        ICategoryService categoryService;
        public ShowProductsByCategory(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        public string Execute(IReadOnlyList<string> parameters)
        {
            var category = this.categoryService.FindByName(parameters[0]);
            if (category == null)
            {
                throw new ArgumentException($"There is no category with name {parameters[0]}");
            }
            var productList = this.productService.ProductsByCategory(category);

            if (productList.Count == 0)
            {
                return $"There are no products in {category.Name} category";
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("Product Name");
                foreach (var product in productList)
                {
                    sb.AppendLine(product.Name);
                }
                return sb.ToString();
            }
        }
    }
}