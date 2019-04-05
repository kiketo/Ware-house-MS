//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using WHMSData.Context;
//using WHMSData.Models;

//namespace WHMS.Services
//{
//    public class ProductWarehouseService : IProductWarehouseService
//    {
//        private readonly WHMSContext context;

//        public ProductWarehouseService(WHMSContext context)
//        {
//            this.context = context ?? throw new ArgumentNullException(nameof(context));
//        }

//        public ProductWarehouse AddQuantity(int productId, int warehouseId, int quantity)
//        {
//            var pairPW = this.context.ProductWarehouse
//                .Where(p => p.ProductId == productId).ToArray().Where(w=>w.WarehouseId == warehouseId).FirstOrDefault();
//            if (pairPW == null )
//            {
//                throw new ArgumentException($"Product or warehouse does not exist");
//            }
//            pairPW.Quantity += quantity;

//            this.context.ProductWarehouse.Update(pairPW);
//            this.context.SaveChanges();

//            return pairPW;
//        }
//        public ProductWarehouse SubstractQuantity(int productId, int warehouseId, int quantity)
//        {
//            var pairPW = this.context.ProductWarehouse
//                .Where(p => p.ProductId == productId).ToArray().Where(w => w.WarehouseId == warehouseId).FirstOrDefault();
//            if (pairPW == null)
//            {
//                throw new ArgumentException($"Product or warehouse does not exist");
//            }
//            if (quantity > pairPW.Quantity)
//            {
//                throw new ArgumentException($"In warehouse { this.context.Warehouses.Where(i=>i.Id == warehouseId).FirstOrDefault().Name} " +
//                    $"the quantity of product {this.context.Products.Where(i => i.Id == productId).FirstOrDefault().Name} " +
//                    $"is less than the wanted amount");
//            }
//            pairPW.Quantity -= quantity;

//            this.context.ProductWarehouse.Update(pairPW);
//            this.context.SaveChanges();

//            return pairPW;
//        }
//        public int GetQuantity(int productId, int warehouseId)//, int quantity)
//        {
//            var pairPW = this.context.ProductWarehouse
//                .Where(p => p.ProductId == productId).ToArray().Where(w => w.WarehouseId == warehouseId).FirstOrDefault();
//            if (pairPW == null)
//            {
//                throw new ArgumentException($"Product or warehouse does not exist");
//            }
//            return pairPW.Quantity;
//        }
//        public ICollection<ProductWarehouse> GetAllProductsInWarehouse(int warehouseId)
//        {
//            return this.context.ProductWarehouse.Where(w => w.WarehouseId == warehouseId).ToList();
//        }
//    }
//}
