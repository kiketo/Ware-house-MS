using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Models;

namespace WHMS.Commands.Modifying
{
    public class ModifyAddressCommand : ICommand
    {
        private IAddressSevice addressSevice;
        private ITownService townService;

        public ModifyAddressCommand(IAddressSevice addressSevice, ITownService townService)
        {
            this.addressSevice = addressSevice ?? throw new ArgumentNullException(nameof(addressSevice));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }
        
        public string Execute(IReadOnlyList<string> parameters)
        {
            if (parameters.Count==3)//Town;oldAddress;newAddress
            {
                Town town = townService.GetTown(parameters[0]);
                Address Address = addressSevice.EditText(town,parameters[1],parameters[2]);
                return $"Addres: {parameters[1]} in Town: {parameters[0]} was modified to Address: {parameters[2]}";
            }
            else if (parameters.Count == 4)//oldTown;oldAddress;newTown;newAddress
            {
                Town oldTown = townService.GetTown(parameters[0]);
                Address address = addressSevice.EditText(oldTown, parameters[1], parameters[3]);
                Town newTown= townService.GetTown(parameters[2]);
                Address newAddress = addressSevice.EditTown(oldTown, parameters[3], newTown);
                return $"Addres: {parameters[1]} in Town: {parameters[0]} was modified " +
                    $"to Address: {parameters[3]} in Town: {parameters[2]}";
            }

            throw new ArgumentException(@"Please provide parameters: Town;oldAddress;newAddress or oldTown;oldAddress;newTown;newAddress");
        }
    }
}
