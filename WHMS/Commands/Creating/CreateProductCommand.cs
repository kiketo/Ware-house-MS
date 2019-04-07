using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Commands.Creating
{
    public class CreateProductCommand : ICommand
    {
        IProductService productService;
        IUnitService unitService;
        ICategoryService categoryService;

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
            var unit = this.unitService.GetUnit(parameters[1]);
            if (unit == null)
            {
                unit = this.unitService.CreateUnit(parameters[1]);
            }
            var category = this.categoryService.GetCategory(parameters[2]);
            if (category == null)
            {
                category = this.categoryService.CreateCategory(parameters[2]);
            }
            var buyPrice = decimal.Parse(parameters[3]);
            var margin = double.Parse(parameters[4]);
            var description = parameters[5];
            var newProduct = this.productService.CreateProduct(name, unit,category,buyPrice,margin, description);

            return $"Product {newProduct.Name} was created.";
        }
    }
}

//Name = name,
//                CreatedOn = DateTime.Now,
//                ModifiedOn = DateTime.Now,
//                BuyPrice = 0,
//                MarginInPercent = 0,
//                SellPrice = 0,
//public override string Execute(IList<string> parameters)
//{
//    var name = parameters[0];
//    var unitExists = this.WarehouseContext.Units.FirstOrDefault(u => u.UnitName == parameters[1]);
//    if (unitExists == null)
//    {
//        unitExists = new Unit()
//        {

//            CreatedOn = DateTime.Now,
//            ModifiedOn = DateTime.Now,
//            UnitName = parameters[1]

//        };
//        this.WarehouseContext.Units.Add(unitExists);
//        this.WarehouseContext.SaveChanges();
//    }
//    var buyPrice = decimal.Parse(parameters[2]);
//    var margin = double.Parse(parameters[3]);

//    var newProduct = new Product()
//    {
//        CreatedOn = DateTime.Now,
//        ModifiedOn = DateTime.Now,
//        Name = name,
//        BuyPrice = buyPrice,
//        MarginInPercent = margin,
//        UnitId = unitExists.Id

//    };


//    this.WarehouseContext.Products.Add(newProduct);
//    this.WarehouseContext.SaveChanges();
//    return "Success";
//}