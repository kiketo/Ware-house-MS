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

        public Address EditText(Town town, string addressToEdit)
        {
            Address address = town.Addresses.FirstOrDefault(a => a.Text == addressToEdit);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToEdit}` in town `{town}` doesn't exist!");
            }

            address.Text = addressToEdit;
            address.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return address;
        }

        public Address EditTown(Town oldTown, string addressToEdit, Town newTown)
        {
            Address address = oldTown.Addresses.FirstOrDefault(a => a.Text == addressToEdit);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToEdit}` in town `{oldTown}` doesn't exist!");
            }

            address.Town = newTown;
            address.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return address;
        }

        public Address Delete(Town town, string addressToDelete)
        {
            Address address = town.Addresses.FirstOrDefault(a => a.Text == addressToDelete);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToDelete}` in town `{town}` doesn't exist!");
            }

            address.IsDeleted = true;
            address.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return address;
        }

    }
}
