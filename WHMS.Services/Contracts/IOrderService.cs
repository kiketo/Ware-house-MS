using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Contracts
{
    public interface IOrderService
    {
        Task<Order> AddAsync(OrderType type, Partner partner, IDictionary<ProductWarehouse, int> pws, ApplicationUser user, string comment = null);

        Task<Order> UpdateAsync(Order order);

        //Task<Order> EditTypeAsync(int orderId, OrderType type);

        //Task<Order> EditPartnerAsync(int orderId, Partner newPartner);

        //Task<Order> AddProductToOrderAsync(int orderId, ProductWarehouse pw);

        //Task<Order> EditCommentAsync(int orderId, string comment);

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<ICollection<Order>> GetOrdersByTypeAsync(OrderType type, DateTime fromDate, DateTime toDate);

        Task<ICollection<Order>> GetOrdersByPartnerAsync(Partner partner);

        Task<Order> DeleteOrderAsync(int id);

        Task<List<Order>> GetOrdersByCreatorId(string userId);
    }
}
