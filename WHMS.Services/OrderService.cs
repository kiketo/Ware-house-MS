using Microsoft.EntityFrameworkCore;
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

        public Order Add(OrderType type, Partner partner, Product product, int qty, string comment = null)
        {
            decimal totalValue;
            if (type==OrderType.Buy)
            {
                totalValue = product.BuyPrice * qty;
            }
            else
            {
                totalValue = product.SellPrice * qty;
            }

            Order newOrder = new Order
            {
                CreatedOn = DateTime.Now,
                Type = type,
                Partner = partner,
                Comment = comment,
                ModifiedOn = DateTime.Now,
                Products = new List<Product> { product },
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

        public Order AddProductToOrder(int orderId, Product product, int qty)
        {
            Order orderToEdit = GetOrderById(orderId);

            decimal totalValue = orderToEdit.TotalValue;

            totalValue += product.SellPrice * qty;

            orderToEdit.Products.Add(product);
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
            Order orderToShow = this.context.Orders
                .Include(p=>p.Products)
                .FirstOrDefault(t => t.Id == orderId);
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
                .Where(o => o.CreatedOn >= fromDate)
                .Where(o => o.CreatedOn <= toDate)
                .Include(p=>p.Partner)
                .Include(p=>p.Products)
                .ToList();

            if (ordersToShow.Count == 0)
            {
                throw new ArgumentException($"Order with Type: {type} from date {fromDate} to date {toDate} doesn't exist!");
            }
            return ordersToShow;
        }

        public ICollection<Order> GetOrdersByPartner(Partner partner)
        {
            List<Order> ordersToShow = this.context.Orders
                .Where(o => o.Partner == partner)
                .Where(o => o.IsDeleted == false)
                .Include(p => p.Partner)
                .Include(p => p.Products)
                .ToList();

            if (ordersToShow.Count == 0)
            {
                throw new ArgumentException($"Order with Type: {partner} doesn't exist!");
            }
            return ordersToShow;
        }
    }
}
