using System;
using System.Collections.Generic;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreateAddressCommand : ICommand
    {
        private IAddressSevice addressSevice;
        private ITownService townService;

        public CreateAddressCommand(IAddressSevice addressSevice, ITownService townService)
        {
            this.addressSevice = addressSevice ?? throw new ArgumentNullException(nameof(addressSevice));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }

        //createaddress;Sofia;jk Hristo Smirnenski bl. 1
        public string Execute(IReadOnlyList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                throw new ArgumentException(@"Please provide parameters: Town;Address");
            }
            var town = townService.GetTown(parameters[0]);

            var address = this.addressSevice.Add(town, parameters[1]);

            return $"Address {address.Text} in town {town.Name} was created.";
        }
    }
}
