using System;
using System.Linq;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class AddressSevice : IAddressSevice
    {
        private readonly ApplicationDbContext context;

        public AddressSevice(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Address Add(Town town, string addressToAdd)
        {
            Address newAddress = town.Addresses.FirstOrDefault(a => a.Text == addressToAdd);

            if (newAddress != null)
            {
                if (newAddress.IsDeleted)
                {
                    newAddress.CreatedOn = DateTime.Now;
                    newAddress.ModifiedOn = DateTime.Now;
                    newAddress.Text = addressToAdd;
                    newAddress.Town = town;
                    newAddress.TownId = town.Id;
                }
                else
                {
                    throw new ArgumentException($"Address `{addressToAdd}` in town `{town.Name}` already exist!");
                }
            }
            else
            {
                newAddress = new Address
                {
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    Text = addressToAdd,
                    Town = town,
                    TownId = town.Id
                };
                this.context.Addresses.Add(newAddress);
            }

            town.Addresses.Add(newAddress);
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
