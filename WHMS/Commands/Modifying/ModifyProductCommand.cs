using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Modifying
{
    public class ModifyProductCommand :ICommand
    {
        IProductService productService;
        ICategoryService categoryService;
        IUnitService unitService;
        public ModifyProductCommand(IProductService productService, IUnitService unitService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.unitService = unitService;
        }

        public string Execute(IReadOnlyList<string> parameters) //whatToModify, nameofproductTobeModified, new parameter
        {
            var toModifyParameter = parameters[0];
            var productToModify = this.productService.FindByName(parameters[1]);
            if (productToModify == null)
            {
                throw new ArgumentException($"Product with name {parameters[1]} does not exist ");
            }
            switch (toModifyParameter)
            {
                case "name":
                    var newName = parameters[2];
                    productToModify = this.productService.ModifyProductName(parameters[1], newName);
                    return $"Product name is successfully changed to {productToModify.Name}";
                case "buyprice":
                    decimal newBuyPrice;
                    if (!decimal.TryParse(parameters[2],out newBuyPrice))
                    {
                        throw new ArgumentException("Entered input is invalid number");
                    }
                    productToModify = this.productService.SetBuyPrice(productToModify.Id, newBuyPrice);
                    return $"Product {productToModify.Name} price was changed to {productToModify.BuyPrice}";
                case "margin":
                    double newMargin;
                    if (!double.TryParse(parameters[2], out newMargin))
                    {
                        throw new ArgumentException("Entered input is invalid number");
                    }
                    productToModify = this.productService.SetMargin(productToModify.Id, newMargin);
                    return $"Product {productToModify.Name} margin price was changed to {productToModify.MarginInPercent}";
                case "category":
                    var category = this.categoryService.FindByName(parameters[2]);
                    if (category == productToModify.Category)
                    {
                        throw new ArgumentException($"Product {productToModify.Name} is already assigned in category {category.Name}");
                    }
                    if (category == null)
                    {
                        category = this.categoryService.CreateCategory(parameters[2]);
                    }
                    this.productService.ModifyCategory(productToModify, category);
                    return $"Product {productToModify.Name} category was assigned in {productToModify.Category.Name}";
                case "unit":
                    var unit = this.unitService.GetUnit(parameters[2]);
                    if (unit == productToModify.Unit)
                    {
                        throw new ArgumentException($"Product {productToModify.Name} already uses unit type {unit.UnitName}");
                    }
                    if (unit == null)
                    {
                        unit = this.unitService.CreateUnit(parameters[2]);
                    }
                    this.productService.ModifyUnit(productToModify, unit);
                    return $"Product {productToModify.Name} unit type was changed to {productToModify.Unit.UnitName}";
                default:
                    return $"Parameter '{parameters[0]}' is not valid. Plase specify: name, buyprice, margin, category or unit.";
            }
        }
    }
}
