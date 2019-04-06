using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Models;

namespace WHMS.Commands.Creating
{
    public class CreatePartnerCommand : ICommand
    {
        private IPartnerService partnerService;
        private IAddressSevice addressSevice;
        private ITownService townService;

        public CreatePartnerCommand(IPartnerService partnerService, IAddressSevice addressSevice, ITownService townService)
        {
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.addressSevice = addressSevice ?? throw new ArgumentNullException(nameof(addressSevice));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }

        //createpartner;name;town;address;VAT
        public string Execute(IReadOnlyList<string> parameters)
        {
            int vat;
            bool isInt;
            Partner partner;
            Town town;
            Address address;
            List<char> number;
            string numberString;
            try
            {
                switch (parameters.Count)
                {
                    case 1://name
                        partner = this.partnerService.Add(parameters[0]);
                        return $"Partner {partner.Name} was created.";
                    case 2://name;VAT
                        number = parameters[1].ToList().Skip(2).ToList();
                        numberString = string.Join("",number);
                        isInt = int.TryParse(numberString, out vat);
                        if (isInt&&number.Count==9)
                        {
                            partner = this.partnerService.Add(parameters[0], vat: parameters[1]);
                            return $"Partner {partner.Name} with VAT: {partner.VAT} was created.";
                        }
                        else
                        {
                            throw new ArgumentException("Please provide a valid VAT number!");
                        }
                    case 3://name;town;adddress
                        town = this.townService.GetTown(parameters[1]);
                        address = this.addressSevice.GetAddress(town, parameters[2]);
                        partner = partnerService.Add(parameters[0], address);
                        return $"Partner {partner.Name} with address: {address.Text} in town: {town.Name} was created.";
                    case 4: //name;town;address;vat
                        number = parameters[3].ToList().Skip(2).ToList();
                        numberString = string.Join("", number);
                        isInt = int.TryParse(numberString, out vat);
                        if (!isInt)
                        {
                            throw new ArgumentException("Please provide a valid VAT number!");
                        }
                        town = this.townService.GetTown(parameters[1]);
                        address = this.addressSevice.GetAddress(town, parameters[2]);
                        partner = partnerService.Add(parameters[0], address, parameters[3]);
                        return $"Partner: {partner.Name} with address: {address.Text} in town: {town.Name} and VAT: {partner.VAT} was created.";
                    default:
                        throw new ArgumentException(@"Please provide parameter(s): {Partner Name};[{Town};{Address}];[VAT]");
                }
            }
            catch (ArgumentException ex)
            {

                throw new ArgumentException(ex.Message+"\n\rNew partner was NOT created!");
            }
        }
    }
}