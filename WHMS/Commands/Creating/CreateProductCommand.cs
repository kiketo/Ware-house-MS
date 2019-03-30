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

        public override string Execute(IList<string> parameters)
        {
            var newProduct = new Product
            {
                //TODO
            };


            this.WarehouseContext.Products.Add(newProduct);
            this.WarehouseContext.SaveChanges();
            throw new NotImplementedException();
        }


    }
}
