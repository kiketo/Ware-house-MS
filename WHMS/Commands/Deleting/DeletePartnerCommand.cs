using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Core.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Models;

namespace WHMS.Commands.Deleting
{
    public class DeletePartnerCommand : ICommand
    {
        IOrderService orderService;
        IPartnerService partnerService;
        IWriter writer;
        IReader reader;

        public DeletePartnerCommand(IOrderService orderService, IPartnerService partnerService, IWriter writer, IReader reader)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }



        //deletepartner;partner
        public string Execute(IReadOnlyList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(@"Please provide valid parameter {partner name}.");
            }

            Partner partnerToDelete = partnerService.FindByName(parameters[0]);

            if (partnerToDelete.PastOrders.Count!=0)
            {
                throw new ArgumentException($"Partner: {partnerToDelete.Name} have past orders and can't be deleted.");
            }

            var sb = new StringBuilder();
            sb.Append("Partner: ");
            sb.AppendLine(partnerToDelete.Name);
            
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine("Do you want to delete? [Y/N]");

            var choice = this.reader.ReadKey();
            this.writer.WriteLine();
            while (true)
            {
                if (choice.KeyChar == 'y')
                {
                    this.partnerService.Delete(partnerToDelete.Name);

                    return $"Partner {parameters[0]} was deleted";
                }
                else if (choice.KeyChar == 'n')
                {
                    return $"Partner {partnerToDelete.Name} was not deleted";
                }
                else
                {
                    this.writer.WriteLine("Enter Valid Key [Y/N]");
                    choice = this.reader.ReadKey();
                    this.writer.WriteLine();
                }
            }
        }
    }
}
