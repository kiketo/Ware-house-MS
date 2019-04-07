using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Commands.Creating
{
    public class CreateOrderCommand : ICommand
    {
        IOrderService orderService;
        IProductService productService;
        IPartnerService partnerService;
        IProductWarehouseService productWarehouseService;
        IWarehouseService warehouseService;

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
        public string Execute(IReadOnlyList<string> parameters) //type,partner,product,quantity,,warehouse, comment
        {
            OrderType orderType = (OrderType)Enum.Parse(typeof(OrderType),parameters[0],true);
            var partner = this.partnerService.FindByName(parameters[1]);
            
            var product = this.productService.FindByName(parameters[2]);
            int quantity;
            if (!int.TryParse(parameters[3], out quantity))
            {
                throw new ArgumentException($"{parameters[2]} is not a number");
            }

            var warehouse =this.warehouseService.GetByName( parameters[4]);
            var inStock = this.productWarehouseService.GetQuantity(product.Id, warehouse.Id);
            if (orderType.Equals("sell"))
            {
                
                if (inStock<quantity)
                {
                    throw new ArgumentException($"The wanted quantity of {product.Name} is not available in {warehouse.Name}");
                }
            }
            else
            {
                this.productWarehouseService.AddQuantity(product.Id, warehouse.Id, quantity);
            }
            var comment = parameters[5];
            var order = this.orderService.Add(orderType, partner, product, quantity, comment);
            partner.PastOrders.Add(order);

            return "";
        }
    }
}
