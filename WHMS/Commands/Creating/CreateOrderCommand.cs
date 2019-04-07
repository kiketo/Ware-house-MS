using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Utills;

namespace WHMS.Commands.Creating
{
    public class CreateOrderCommand : ICommand
    {
        IOrderService orderService;
        IProductService productService;
        public CreateOrderCommand(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }
        public string Execute(IReadOnlyList<string> parameters)
        {

            return "";
        }
    }
}
