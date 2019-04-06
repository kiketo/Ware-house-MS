using System.Collections.Generic;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Contracts
{
    public interface IOrderService
    {
        Order Add(OrderType type, Partner partner, IDictionary<Product, int> products, string comment = null);

        Order EditType(int orderId, OrderType type);

        Order EditPartner(int orderId, Partner newPartner);

        Order EditProducts(int orderId, IDictionary<Product, int> products);

        Order EditComment(int orderId, string comment);

        Order GetOrderById(int orderId);

        ICollection<Order> GetOrdersByType(OrderType type);

        ICollection<Order> GetOrdersByPartner(Partner partner);
    }
}
