using System;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WHMS.Services
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext context;

        public AddressService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Address> AddAsync(Town town, string addressToAdd)
        {
            Address newAddress = town.Addresses.FirstOrDefault(a => a.Text == addressToAdd);

            if (newAddress != null)
            {
                if (newAddress.IsDeleted)
                {
                    newAddress.IsDeleted = false;
                    newAddress.CreatedOn = DateTime.Now;
                    newAddress.ModifiedOn = DateTime.Now;
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
            await this.context.SaveChangesAsync();

            return newAddress;
        }

        public async Task<Address> EditTextAsync(Town town, string oldAddress, string newAddress)
        {
            Address address = town.Addresses.FirstOrDefault(a => a.Text == oldAddress);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{oldAddress}` in town `{town.Name}` doesn't exist!");
            }

            address.Text = newAddress;
            address.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();

            return address;
        }

        public async Task<Address> EditTownAsync(Town oldTown, string address, Town newTown)
        {
            Address addressToEdit = oldTown.Addresses.FirstOrDefault(a => a.Text == address);

            if (addressToEdit == null || addressToEdit.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToEdit}` in town `{oldTown.Name}` doesn't exist!");
            }

            addressToEdit.Town = newTown;
            addressToEdit.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();

            return addressToEdit;
        }

        public async Task<Address> DeleteAsync(Town town, string addressToDelete)
        {
            Address address = town.Addresses.FirstOrDefault(a => a.Text == addressToDelete);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToDelete}` in town `{town.Name}` doesn't exist!");
            }

            address.IsDeleted = true;
            address.ModifiedOn = DateTime.Now;

           await this.context.SaveChangesAsync();

            return address;
        }

        public async Task<Address> GetAddressAsync(Town town, int addressId)
        {
            Address address = await this.context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId && a.Town == town);

            return address;
        }
        public async Task<List<Address>> GetAllAddressesAsync()
        {
            var addresses = await this.context.Addresses
                .Include(a=>a.Town)
                .ToListAsync();

           
            return addresses;
        }
        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            var address = await this.context.Addresses.FirstOrDefaultAsync(i => i.Id == addressId);
            return address;
        }
    }
}
