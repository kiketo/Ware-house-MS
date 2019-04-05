//using System;
//using System.Linq;
//using WHMS.Services.Interfaces;
//using WHMSData.Context;
//using WHMSData.Models;

//namespace WHMS.Services
//{
//    public class ProductOrderWarehouseService : IProductOrderWarehouseService
//    {
//        private readonly WHMSContext context;

//        public ProductOrderWarehouseService(WHMSContext context)
//        {
//            this.context = context ?? throw new ArgumentNullException(nameof(context));
//        }

//        public ProductOrderWarehouse Add(int productId, int orderId, int warehouseId, int quantity)
//        {
//            if (context.ProductOrderWarehouse
//                .FirstOrDefault(p => p.ProductId == productId && p.OrderId == orderId && p.WarehouseId == warehouseId) != null)
//            {
//                throw new ArgumentException($"ProductOrderWarehouse with ProductID: {productId}, OrderID: {orderId} and WarehouseID: {warehouseId} already exist!");
//            }
//            ProductOrderWarehouse newRecord = new ProductOrderWarehouse
//            {
//                ProductId = productId,
//                OrderId = orderId,
//                WarehouseId = warehouseId,
//                Quantity = quantity
//            };

//            context.ProductOrderWarehouse.Add(newRecord);
//            context.SaveChanges();

//            return newRecord;
//        }

//        public ProductOrderWarehouse AddQuantity(int productId, int orderId, int warehouseId, int quantity)
//        {
//            ProductOrderWarehouse recordToEdit = context.ProductOrderWarehouse
//                .FirstOrDefault(p => p.ProductId == productId && p.OrderId == orderId && p.WarehouseId == warehouseId);

//            if (recordToEdit == null || recordToEdit.IsDeleted)
//            {
//                throw new ArgumentException($"ProductOrderWarehouse with ProductID: {productId}, OrderID: {orderId} and WarehouseID: {warehouseId} doesn't exist!");
//            }

//            recordToEdit.Quantity += quantity;

//            context.SaveChanges();

//            return recordToEdit;
//        }

//        public ProductOrderWarehouse SubstractQuantity(int productId, int orderId, int warehouseId, int quantity)
//        {
//            ProductOrderWarehouse recordToEdit = context.ProductOrderWarehouse
//                .FirstOrDefault(p => p.ProductId == productId && p.OrderId == orderId && p.WarehouseId == warehouseId);

//            if (recordToEdit == null || recordToEdit.IsDeleted)
//            {
//                throw new ArgumentException($"ProductOrderWarehouse with ProductID: {productId}, OrderID: {orderId} and WarehouseID: {warehouseId} doesn't exist!");
//            }

//            if (recordToEdit.Quantity<quantity)
//            {
//                throw new ArgumentException($"In warehouse { this.context.Warehouses.Where(i => i.Id == warehouseId).FirstOrDefault().Name} " +
//                    $"the quantity of product {this.context.Products.Where(i => i.Id == productId).FirstOrDefault().Name} " +
//                    $"is less than the wanted amount");
//            }

//            recordToEdit.Quantity -= quantity;

//            context.SaveChanges();

//            return recordToEdit;
//        }

//        public void Delete(int productId, int orderId, int warehouseId)
//        {
//            ProductOrderWarehouse recordToDelete = context.ProductOrderWarehouse
//                .FirstOrDefault(p => p.ProductId == productId && p.OrderId == orderId && p.WarehouseId == warehouseId);

//            if (recordToDelete == null || recordToDelete.IsDeleted)
//            {
//                throw new ArgumentException($"ProductOrderWarehouse with ProductID: {productId}, OrderID: {orderId} and WarehouseID: {warehouseId} doesn't exist!");
//            }

//            recordToDelete.IsDeleted = true;
//            context.SaveChanges();

//            return;
//        }

//        public ProductOrderWarehouse EditQuantity(int productId, int orderId, int warehouseId, int newQuantity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
