using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Interfaces;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services
{
    public class OrderService : IOrderService
    {
        private readonly WHMSContext context;

        public OrderService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order Add(OrderType type, Partner partner, ICollection<ProductOrderWarehouse> productsWarehouses, string comment = null)
        {
            decimal totalValue = 0;
            foreach (var productWarehouse in productsWarehouses)
            {
                totalValue += productWarehouse.Quantity * productWarehouse.Product.SellPrice;
            }

            Order newOrder = new Order
            {
                CreatedOn = DateTime.Now,
                Type = type,
                Partner = partner,
                Comment = comment,
                ProductsAndWarehouses = productsWarehouses,
                TotalValue = totalValue
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();
            return newOrder;
        }

        public Order EditType(int orderId, OrderType type)
        {
            //Order orderToEdit = context.Orders.FirstOrDefault(t => t.Id == orderId);
            //if (orderToEdit==null||orderToEdit.IsDeleted)
            //{
            //    throw new ArgumentException($"Order with ID: {orderId} doesn't exist!");
            //}
            Order orderToEdit = GetOrder(orderId);

            orderToEdit.Type = type;
            orderToEdit.ModifiedOn = DateTime.Now;

            context.SaveChanges();
            return orderToEdit;
        }

        public Order EditPartner(int orderId, Partner newPartner)
        {
            Order orderToEdit = GetOrder(orderId);

            orderToEdit.Partner = newPartner;
            orderToEdit.ModifiedOn = DateTime.Now;

            context.SaveChanges();
            return orderToEdit;
        }

        public Order EditProductsWarehouses(int orderId, ICollection<ProductOrderWarehouse> productsWarehouses)
        {
            Order orderToEdit = GetOrder(orderId);

            orderToEdit.ProductsAndWarehouses = productsWarehouses;
            orderToEdit.ModifiedOn = DateTime.Now;

            context.SaveChanges();
            return orderToEdit;
        }

        public Order EditProductsWarehouses(int orderId, string comment)
        {
            Order orderToEdit = GetOrder(orderId);

            orderToEdit.Comment = comment;
            orderToEdit.ModifiedOn = DateTime.Now;

            context.SaveChanges();
            return orderToEdit;
        }

        private Order GetOrder(int orderId)
        {
            Order orderToEdit = context.Orders.FirstOrDefault(t => t.Id == orderId);
            if (orderToEdit == null || orderToEdit.IsDeleted)
            {
                throw new ArgumentException($"Order with ID: {orderId} doesn't exist!");
            }
            return orderToEdit;
        }


    }
}
