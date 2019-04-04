using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Services.Interfaces;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class ProductOrderWarehouseService : IProductOrderWarehouseService
    {
        private readonly WHMSContext context;

        public ProductOrderWarehouseService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ProductOrderWarehouse Add(int productId, int orderId, int warehouseId, int quantity)
        {
            if (context.ProductOrderWarehouse
                .FirstOrDefault(p => p.ProductId == productId && p.OrderId == orderId && p.WarehouseId == warehouseId) != null)
            {
                throw new ArgumentException($"ProductOrderWarehouse with ProductID: {productId}, OrderID: {orderId} and WarehouseID: {warehouseId} already exist!");
            }
            ProductOrderWarehouse newRecord = new ProductOrderWarehouse
            {
                ProductId = productId,
                OrderId = orderId,
                WarehouseId = warehouseId,
                Quantity = quantity
            };

            context.ProductOrderWarehouse.Add(newRecord);
            context.SaveChanges();

            return newRecord;
        }

        public ProductOrderWarehouse EditQuantity(int productId, int orderId, int warehouseId, int newQuantity)
        {
            ProductOrderWarehouse recordToEdit = context.ProductOrderWarehouse
                .FirstOrDefault(p => p.ProductId == productId && p.OrderId == orderId && p.WarehouseId == warehouseId);

            if (recordToEdit == null)
            {
                throw new ArgumentException($"ProductOrderWarehouse with ProductID: {productId}, OrderID: {orderId} and WarehouseID: {warehouseId} doesn't exist!");
            }

            recordToEdit.Quantity = newQuantity;

            context.SaveChanges();

            return recordToEdit;
        }

        public void Delete(int productId, int orderId, int warehouseId)
        {
            ProductOrderWarehouse recordToDelete = context.ProductOrderWarehouse
                .FirstOrDefault(p => p.ProductId == productId && p.OrderId == orderId && p.WarehouseId == warehouseId);

            if (recordToDelete == null)
            {
                throw new ArgumentException($"ProductOrderWarehouse with ProductID: {productId}, OrderID: {orderId} and WarehouseID: {warehouseId} doesn't exist!");
            }

            context.ProductOrderWarehouse.Remove(recordToDelete);
            context.SaveChanges();

            return;
        }
    }
}
