using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Commands.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Commands.Creating
{
    public class CreateProductCommand : BaseCommand
    {
        public CreateProductCommand(WHMSContext context) : base(context)
        {
        }
        // TODO product needs: name, Description, unit, category, partners, buyprice, WH

        public override string Execute(IList<string> parameters)
        {
           //Console.WriteLine("Input NameProduct, unitName, Price, Margin:");
            var name = parameters[0];
            var unitExists = this.WarehouseContext.Units.FirstOrDefault(u => u.UnitName == parameters[1]);
            if (unitExists == null)
            {
                unitExists = new Unit()
                {
                    
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    UnitName = parameters[1]
                    
            };
                this.WarehouseContext.Units.Add(unitExists);
                this.WarehouseContext.SaveChanges();
            }
            var buyPrice =decimal.Parse( parameters[2]);
            var margin = double.Parse(parameters[3]);
            
            var newProduct = new Product()
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Name = name,
                BuyPrice= buyPrice,
                MarginInPercent = margin,
                UnitId = unitExists.Id

            };


            this.WarehouseContext.Products.Add(newProduct);
            this.WarehouseContext.SaveChanges();
            return "Success";
        }


    }
}
