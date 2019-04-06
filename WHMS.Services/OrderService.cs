using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Contracts;
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

        public Order Add(OrderType type, Partner partner, IDictionary<Product, int> products, string comment = null)
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
                Products = products.Select(p => p.Key).ToList(),
                TotalValue = totalValue
            };

            this.context.Orders.Add(newOrder);
            this.context.SaveChanges();
            return newOrder;
        }

        public Order EditType(int orderId, OrderType type)
        {
            Order orderToEdit = GetOrderById(orderId);

            orderToEdit.Type = type;
            orderToEdit.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return orderToEdit;
        }

        public Order EditPartner(int orderId, Partner newPartner)
        {
            Order orderToEdit = GetOrderById(orderId);

            orderToEdit.Partner = newPartner;
            orderToEdit.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return orderToEdit;
        }

        public Order EditProducts(int orderId, IDictionary<Product, int> products)
        {
            Order orderToEdit = GetOrderById(orderId);

            decimal totalValue = 0;
            foreach (var product in products)
            {
                totalValue += product.Key.SellPrice * product.Value;
            }

            orderToEdit.Products = products.Select(p => p.Key).ToList();
            orderToEdit.TotalValue = totalValue;
            orderToEdit.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return orderToEdit;
        }

        public Order EditComment(int orderId, string comment)
        {
            Order orderToEdit = GetOrderById(orderId);

            orderToEdit.Comment = comment;
            orderToEdit.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return orderToEdit;
        }

        public Order GetOrderById(int orderId)
        {
            Order orderToShow = this.context.Orders.FirstOrDefault(t => t.Id == orderId);
            if (orderToShow == null || orderToShow.IsDeleted)
            {
                throw new ArgumentException($"Order with ID: {orderId} doesn't exist!");
            }
            return orderToShow;
        }

        public ICollection<Order> GetOrdersByType(OrderType type, DateTime fromDate, DateTime toDate)
        {
            List<Order> ordersToShow = this.context.Orders
                .Where(o => o.Type == type)
                .Where(o => o.IsDeleted == false)
                .Where(o=>o.CreatedOn>=fromDate)
                .Where(o => o.CreatedOn <= toDate)
                .ToList();

            if (ordersToShow == null)
            {
                throw new ArgumentException($"Order with Type: {type} doesn't exist!");
            }
            return ordersToShow;
        }

        public ICollection<Order> GetOrdersByPartner(Partner partner)
        {
            List<Order> ordersToShow = this.context.Orders
                .Where(o => o.Partner == partner)
                .Where(o => o.IsDeleted == false)
                .ToList();

            if (ordersToShow == null)
            {
                throw new ArgumentException($"Order with Type: {partner} doesn't exist!");
            }
            return ordersToShow;
        }
    }
}
