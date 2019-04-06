using System;
using System.Collections.Generic;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Contracts
{
    public interface IOrderService
    {
        Order Add(OrderType type, Partner partner, Product product, int qty, string comment = null);

        Order EditType(int orderId, OrderType type);

        Order EditPartner(int orderId, Partner newPartner);

        Order AddProduct(int orderId, Product product, int qty);

        Order EditComment(int orderId, string comment);

        Order GetOrderById(int orderId);

        ICollection<Order> GetOrdersByType(OrderType type, DateTime fromDate, DateTime toDate);

        ICollection<Order> GetOrdersByPartner(Partner partner);
    }
}
