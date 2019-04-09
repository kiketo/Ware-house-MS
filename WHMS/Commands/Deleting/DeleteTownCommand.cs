using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Core.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Models;

namespace WHMS.Commands.Deleting
{
    public class DeleteTownCommand : ICommand
    {
        private ITownService townService;
        private IWriter writer;
        private IAddressSevice addressSevice;

        public DeleteTownCommand(ITownService townService, IWriter writer, IAddressSevice addressSevice)
        {
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.addressSevice = addressSevice ?? throw new ArgumentNullException(nameof(addressSevice));
        }

        //deletetown;Sofia
        public string Execute(IReadOnlyList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(@"Please provide valid parameter {town}.");
            }

            Town townToDelete = townService.GetTown(parameters[0]);
            var sb = new StringBuilder();
            sb.Append("Town: ");
            sb.AppendLine(townToDelete.Name);
            sb.AppendLine(" with addresses:");
            
            foreach (var address in townToDelete.Addresses)
            {
                if (!address.IsDeleted)
                {
                    sb.AppendLine(address.Text);
                }
            }

            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine("Do you want to delete the Town, together with the addresses? [Y/N]");

            var choice = this.writer.ReadKey();
            this.writer.WriteLine();
            while (true)
            {
                if (choice.KeyChar == 'y')
                {
                    foreach (var address in townToDelete.Addresses)
                    {
                        if (!address.IsDeleted)
                        {
                            addressSevice.Delete(townToDelete, address.Text);
                        }
                    }

                    this.townService.Delete(townToDelete.Name);

                    return $"Town {parameters[0]} was deleted";
                }
                else if (choice.KeyChar == 'n')
                {
                    return $"Town {townToDelete.Name} was not deleted";
                }
                else
                    this.writer.WriteLine("Enter Valid Key [Y/N]");
            }
        }
    }
}
