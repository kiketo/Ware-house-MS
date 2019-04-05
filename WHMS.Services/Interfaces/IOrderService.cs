using System.Collections.Generic;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Interfaces
{
    public interface IOrderService
    {
        Order Add(OrderType type, Partner partner, ICollection<ProductOrderWarehouse> productsWarehouses, string comment = null);

        Order EditType(int orderId, OrderType type);

        Order EditPartner(int orderId, Partner newPartner);

        Order EditProductsWarehouses(int orderId, ICollection<ProductOrderWarehouse> productsWarehouses);

        Order EditComment(int orderId, string comment);
    }
}
