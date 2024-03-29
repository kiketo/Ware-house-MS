﻿using System.Collections.Generic;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreateProductCommand : ICommand
    {
        private IProductService productService;
        private IUnitService unitService;
        private ICategoryService categoryService;

        public CreateProductCommand(IProductService productService, IUnitService unitService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.unitService = unitService;
        }
        // TODO product needs: name, unit, category, buyprice, margin, Description

        public string Execute(IReadOnlyList<string> parameters)
        {
            var name = parameters[0];

            var product = this.productService.FindByNameInclncludingDeleted(name);
            if (product == null)
            {
                var unit = this.unitService.GetUnit(parameters[1]);
                if (unit == null)
                {
                    unit = this.unitService.CreateUnit(parameters[1]);
                }
                var category = this.categoryService.FindByName(parameters[2]);
                if (category == null)
                {
                    category = this.categoryService.CreateCategory(parameters[2]);
                }
                var buyPrice = decimal.Parse(parameters[3]);
                var margin = double.Parse(parameters[4]);
                var description = parameters[5];
                var newProduct = this.productService.CreateProduct(name, unit, category, buyPrice, margin, description);
            }
            else if (product.IsDeleted)
            {
                var recoveredProduct = this.productService.UndeleteProduct(product.Name);
                var unit = this.unitService.GetUnit(parameters[1]);
                if (unit == null)
                {
                    unit = this.unitService.CreateUnit(parameters[1]);
                }
                this.productService.ModifyUnit(recoveredProduct, unit);
                var category = this.categoryService.FindByName(parameters[2]);
                if (category == null)
                {
                    category = this.categoryService.CreateCategory(parameters[2]);
                }
                this.productService.ModifyCategory(recoveredProduct, category);
                var buyPrice = decimal.Parse(parameters[3]);
                this.productService.SetBuyPrice(recoveredProduct.Id, buyPrice);
                var margin = double.Parse(parameters[4]);
                this.productService.SetMargin(recoveredProduct.Id, margin);

                var description = parameters[5];

            }

            return $"Product {name} was created.";
        }
    }
}

