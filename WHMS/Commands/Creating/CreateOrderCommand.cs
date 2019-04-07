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

        public CreateOrderCommand(IOrderService orderService, IProductService productService, IPartnerService partnerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.partnerService = partnerService;
        }
        public string Execute(IReadOnlyList<string> parameters)
        {
            OrderType orderType = (OrderType)Enum.Parse(typeof(OrderType),parameters[0],true);
            var partner = this.partnerService.FindByName(parameters[1]);
            var product = this.productService.FindByName(parameters[2]);
            int quantity;
            if (int.TryParse(parameters[3], out quantity))
            {
                throw new ArgumentException($"{parameters[2]} is not a number");
            }
            var comment = parameters[4];
            var order = this.orderService.Add(orderType, partner, product, quantity, comment);


            return "";
        }
    }
}
