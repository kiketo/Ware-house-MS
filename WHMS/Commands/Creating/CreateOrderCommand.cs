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
            //OrderType type, Partner partner, Product product, int qty, string comment = null)
           //testing perposes: this.orderService.Add(OrderType.Buy, this.partnerService.FindByName("partner"), this.productService.GetProduct("sirene"), 1);
            return "";
        }
    }
}
