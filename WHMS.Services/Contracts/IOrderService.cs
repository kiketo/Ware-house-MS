﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Contracts
{
    public interface IOrderService
    {
        Task<Order> AddAsync(OrderType type, Partner partner, ProductWarehouse pw, int qty, string comment = null);

        Task<Order> EditTypeAsync(int orderId, OrderType type);

        Task<Order> EditPartnerAsync(int orderId, Partner newPartner);

        Task<Order> AddProductToOrderAsync(int orderId, ProductWarehouse pw);

        Task<Order> EditCommentAsync(int orderId, string comment);

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<ICollection<Order>> GetOrdersByTypeAsync(OrderType type, DateTime fromDate, DateTime toDate);

        Task<ICollection<Order>> GetOrdersByPartnerAsync(Partner partner);
    }
}
