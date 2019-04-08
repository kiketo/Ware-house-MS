﻿using System;
using System.Collections.Generic;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Utills;

namespace WHMS.Commands.Creating
{
    public class CreateOrderCommand : ICommand
    {
        private IOrderService orderService;
        private IProductService productService;
        private IPartnerService partnerService;
        private IProductWarehouseService productWarehouseService;
        private IWarehouseService warehouseService;

        public CreateOrderCommand(
            IOrderService orderService,
            IProductService productService,
            IPartnerService partnerService,
            IProductWarehouseService productWarehouseService,
            IWarehouseService warehouseService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.partnerService = partnerService;
            this.productWarehouseService = productWarehouseService;
            this.warehouseService = warehouseService;
        }
        public string Execute(IReadOnlyList<string> parameters) //type,partner,product,quantity,warehouse, comment
        {
            OrderType orderType;
            if (!Enum.TryParse(parameters[0].ToLower(), true, out orderType))
            {
                throw new ArgumentException($"Order type '{parameters[0]}' is not valid. Should be 'buy' or 'sell'.");
            }
            var partner = this.partnerService.FindByName(parameters[1]);

            var product = this.productService.FindByName(parameters[2]);
            int quantity;
            if (!int.TryParse(parameters[3], out quantity))
            {
                throw new ArgumentException($"{parameters[3]} is not a valid quantity.");
            }

            var warehouse = this.warehouseService.GetByName(parameters[4]);
            var inStock = this.productWarehouseService.GetQuantity(product.Id, warehouse.Id);
            if (orderType.Equals("sell"))
            {
                if (inStock < quantity)
                {
                    throw new ArgumentException($"The wanted quantity of {product.Name} is not available in {warehouse.Name}");
                }
                this.productWarehouseService.SubstractQuantity(product.Id, warehouse.Id, quantity);
            }
            else
            {
                this.productWarehouseService.AddQuantity(product.Id, warehouse.Id, quantity);
            }
            string comment=null;
            if (parameters.Count == 6)
            {
                comment = parameters[5];
            }
            var order = this.orderService.Add(orderType, partner, product, quantity, comment);
            partner.PastOrders.Add(order);

            

            return $"Order with ID: {order.Id} was created.";
        }
    }
}
