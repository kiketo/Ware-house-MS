using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class ProductWarehouseService
    {
        private readonly WHMSContext context;

        public ProductWarehouseService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ProductWarehouse AddQuantity(int productId, int warehouseId, int quantity)
        {
            //var pairPW = this.context.ProductWarehouse
            //    .GroupBy(p => p.ProductId == productId && w=>w.wa == warehouseId);



                
            //this.context.Products.Add(newProduct);
            //this.context.SaveChanges();

            return null;
        }

    }
}
