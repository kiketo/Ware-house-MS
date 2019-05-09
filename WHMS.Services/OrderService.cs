using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Order> AddAsync(OrderType type, Partner partner, IDictionary<ProductWarehouse, int> pws, string comment = null)
        {
            decimal totalValue = 0;
            
            if (type == OrderType.Buy)
            {
                foreach (var pw in pws)
                {
                    totalValue = pw.Key.Product.BuyPrice * pw.Value;
                }
                
            }
            else
            {
                foreach (var pw in pws)
                {
                    totalValue = pw.Key.Product.SellPrice * pw.Value;
                }
                
            }

            Order newOrder = new Order
            {
                CreatedOn = DateTime.Now,
                Type = type,
                Partner = partner,
                Comment = comment,
                ModifiedOn = DateTime.Now,
                ProductsWarehouses = new List<ProductWarehouse>(pws.Keys),
                TotalValue = totalValue
            };
            
            await this.context.Orders.AddAsync(newOrder);
            await this.context.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Order> EditTypeAsync(int orderId, OrderType type)
        {
            Order orderToEdit = await GetOrderByIdAsync(orderId);

            orderToEdit.Type = type;
            orderToEdit.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return orderToEdit;
        }

        public async Task<Order> EditPartnerAsync(int orderId, Partner newPartner)
        {
            Order orderToEdit = await GetOrderByIdAsync(orderId);

            orderToEdit.Partner = newPartner;
            orderToEdit.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return orderToEdit;
        }

        public async Task<Order> AddProductToOrderAsync(int orderId, ProductWarehouse pw)
        {
            Order orderToEdit = await GetOrderByIdAsync(orderId);

            decimal totalValue = orderToEdit.TotalValue;

            totalValue +=pw.Product.SellPrice * pw.Quantity;

            orderToEdit.ProductsWarehouses.Add(pw);
            orderToEdit.TotalValue = totalValue;
            orderToEdit.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return orderToEdit;
        }

        public async Task<Order> EditCommentAsync(int orderId, string comment)
        {
            Order orderToEdit = await GetOrderByIdAsync(orderId);

            orderToEdit.Comment = comment;
            orderToEdit.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return orderToEdit;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            Order orderToShow =await this.context.Orders
                .Include(o => o.ProductsWarehouses)
                .Include(o => o.Partner)
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
            if (orderToShow == null || orderToShow.IsDeleted)
            {
                throw new ArgumentException($"Order with ID: {orderId} doesn't exist!");
            }
            return orderToShow;
        }

        public async Task<ICollection<Order>> GetOrdersByTypeAsync(OrderType type, DateTime fromDate, DateTime toDate)
        {
            List<Order> ordersToShow = await this.context.Orders
                .Where(o => o.Type == type)
                .Where(o => o.IsDeleted == false)
                .Where(o => o.CreatedOn >= fromDate)
                .Where(o => o.CreatedOn <= toDate)
                .Include(p => p.Partner)
                .Include(p => p.ProductsWarehouses)
                .ToListAsync();

            if (ordersToShow.Count == 0)
            {
                throw new ArgumentException($"Order with Type: {type} from date {fromDate} to date {toDate} doesn't exist!");
            }
            return ordersToShow;
        }

        public async Task<ICollection<Order>> GetOrdersByPartnerAsync(Partner partner)
        {
            List<Order> ordersToShow =await this.context.Orders
                .Where(o => o.Partner == partner)
                .Where(o => o.IsDeleted == false)
                .Include(p => p.Partner)
                .Include(p => p.ProductsWarehouses)
                .ToListAsync();

            if (ordersToShow.Count == 0)
            {
                throw new ArgumentException($"Orders of Partner: {partner} doesn't exist!");
            }
            return ordersToShow;
        }

        public async Task<Order> DeleteOrderAsync(int id)
        {
            Order orderToDelete = await this.GetOrderByIdAsync(id);
            orderToDelete.IsDeleted = true;
            orderToDelete.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();

            return orderToDelete;
        }
    }
}
