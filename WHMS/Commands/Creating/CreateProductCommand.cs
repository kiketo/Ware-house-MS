using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Commands.Creating
{
    public class CreateProductCommand : BaseCommand
    {
        public CreateProductCommand(IWHMSContext context) : base(context)
        {
        }
        // TODO product needs: name, Description, unit, category, partners, buyprice, WH
        public override string Execute(IList<string> parameters)
        {
            var name = parameters[0];
            var description = parameters[1];
            var newProduct = new Product()
            {
                Name = name,
                Description = description
            };


            this.WarehouseContext.Products.Add(newProduct);
            this.WarehouseContext.SaveChanges();
            return "Success";
        }


    }
}
