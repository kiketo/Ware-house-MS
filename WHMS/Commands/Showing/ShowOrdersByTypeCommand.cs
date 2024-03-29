﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Utills;

namespace WHMS.Commands.Showing
{
    public class ShowOrdersByTypeCommand : ICommand
    {
        private IOrderService orderService;

        public ShowOrdersByTypeCommand(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        //ShowOrdersByType;buy;01/01/2018;01/01/2018;
        public string Execute(IReadOnlyList<string> parameters)
        {
            DateTime fromDate;
            DateTime toDate;
            if (parameters.Count != 3&& parameters.Count != 1)
            {
                throw new ArgumentException(@"Please provide Parameters:  Type;[fromDate;toDate]");
            }
            OrderType typeOrder = 0;
            bool tryEnum = Enum.TryParse(parameters[0], true, out typeOrder);
            if (!tryEnum)
            {
                throw new ArgumentException($"Invalid Order Type: {parameters[0]}");
            }


            if (parameters.Count == 1)
            {
                fromDate = DateTime.MinValue;
                toDate = DateTime.MaxValue;
            }
            else
            {
                bool tryFromDate = DateTime.TryParse(parameters[1], out fromDate);
                if (!tryFromDate)
                {
                    throw new ArgumentException($"Invalid From Date: {parameters[1]}");
                }

                bool tryToDate = DateTime.TryParse(parameters[2], out toDate);
                if (!tryToDate)
                {
                    throw new ArgumentException($"Invalid To Date: {parameters[2]}");
                }
            }

            var orders = orderService.GetOrdersByType(typeOrder, fromDate, toDate);

            var result = new StringBuilder();
            result.AppendLine($"Found {orders.Count} orders");
            result.Append(string.Join(Environment.NewLine, orders
                .Select(o => $"Id: {o.Id} \r\n Created on: {o.CreatedOn} \r\n Partner: {o.Partner.Name} \r\n" +
                $"  Products: {string.Join(Environment.NewLine, o.Products.Select(p => $"{p.Name}"))}")));

            return result.ToString();
        }
    }
}
