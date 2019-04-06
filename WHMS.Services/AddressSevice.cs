using System;
using System.Linq;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class AddressSevice : IAddressSevice
    {
        private readonly WHMSContext context;

        public AddressSevice(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Address Add(Town town, string addressToAdd)
        {
            
            if (town.Addresses!=null&&town.Addresses.FirstOrDefault(a => a.Text == addressToAdd) != null)
            {
                throw new ArgumentException($"Address `{addressToAdd}` in town `{town.Name}` already exist!");
            }

            Address newAddress = new Address
            {
                CreatedOn = DateTime.Now,
                ModifiedOn=DateTime.Now,
                Text = addressToAdd,
                Town = town,
                TownId = town.Id
            };

            town.Addresses.Add(newAddress);
            this.context.Addresses.Add(newAddress);
            this.context.SaveChanges();

            return newAddress;
        }

        public Address EditText(Town town, string oldAddress, string newAddress)
        {
            Address address = town.Addresses.FirstOrDefault(a => a.Text == oldAddress);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{oldAddress}` in town `{town.Name}` doesn't exist!");
            }

            address.Text = newAddress;
            address.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return address;
        }

        public Address EditTown(Town oldTown, string address, Town newTown)
        {
            Address addressToEdit = oldTown.Addresses.FirstOrDefault(a => a.Text == address);

            if (addressToEdit == null || addressToEdit.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToEdit}` in town `{oldTown.Name}` doesn't exist!");
            }

            addressToEdit.Town = newTown;
            addressToEdit.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return addressToEdit;
        }

        public Address Delete(Town town, string addressToDelete)
        {
            Address address = town.Addresses.FirstOrDefault(a => a.Text == addressToDelete);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToDelete}` in town `{town.Name}` doesn't exist!");
            }

            address.IsDeleted = true;
            address.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return address;
        }

        public Address GetAddress(Town town, string addressToGet)
        {
            Address address = town.Addresses.FirstOrDefault(a => a.Text == addressToGet);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToGet}` in town `{town.Name}` doesn't exist!");
            }

            return address;
        }

    }
}
