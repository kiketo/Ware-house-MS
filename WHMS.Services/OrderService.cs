using Microsoft.EntityFrameworkCore;
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

        public Order Add(OrderType type, Partner partner, IDictionary<Product,int> products, string comment = null)
        {
            decimal totalValue = 0;
            foreach (var product in products)
            {
                totalValue += product.Key.SellPrice * product.Value;
            }

            Order newOrder = new Order
            {
                CreatedOn = DateTime.Now,
                Type = type,
                Partner = partner,
                Comment = comment,
                ModifiedOn = DateTime.Now,
                Products=products.Select(p=>p.Key).ToList(),
                TotalValue = totalValue
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();
            return newOrder;
        }

        public Order EditType(int orderId, OrderType type)
        {
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

        public Order EditProducts(int orderId, IDictionary<Product, int> products)
        {
            Order orderToEdit = GetOrder(orderId);

            decimal totalValue = 0;
            foreach (var product in products)
            {
                totalValue += product.Key.SellPrice * product.Value;
            }

            orderToEdit.Products = products.Select(p=>p.Key).ToList();
            orderToEdit.TotalValue = totalValue;
            orderToEdit.ModifiedOn = DateTime.Now;

            context.SaveChanges();
            return orderToEdit;
        }

        public Order EditComment(int orderId, string comment)
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
