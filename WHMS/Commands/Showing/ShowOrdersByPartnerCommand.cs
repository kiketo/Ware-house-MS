using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Models;

namespace WHMS.Commands.Showing
{
    public class ShowOrdersByPartnerCommand : ICommand
    {
        private IOrderService orderService;
        private IPartnerService partnerService;

        public ShowOrdersByPartnerCommand(IOrderService orderService, IPartnerService partnerService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
        }

        //ShowOrdersByPartner;Partner
        public string Execute(IReadOnlyList<string> parameters)
        {
            if ( parameters.Count != 1)
            {
                throw new ArgumentException(@"Please provide parameter: Partner");
            }

            Partner partner = partnerService.FindByName(parameters[0]);

            var orders = orderService.GetOrdersByPartner(partner);

            var result = new StringBuilder();
            result.AppendLine($"Found {orders.Count} orders");
            result.Append(string.Join(Environment.NewLine, orders
                .Select(o => $"Id: {o.Id} \r\n Created on: {o.CreatedOn} \r\n Partner: {o.Partner.Name} \r\n" +
                $"  Products: {string.Join(Environment.NewLine, o.Products.Select(p => $"{p.Name}"))}")));

            return result.ToString();
        }
    }
}
