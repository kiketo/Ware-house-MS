﻿using System;
using System.Linq;
using WHMS.Services.Interfaces;
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

        public Address Add(string town, string addressToAdd)
        {
            Town inTown = this.context.Towns.FirstOrDefault(t => t.Name == town);
            if (inTown == null || inTown.IsDeleted)
            {
                throw new ArgumentException($"Town `{town}` doesn't exist!");
            }
            if (inTown.Addresses.FirstOrDefault(a => a.Text == addressToAdd) != null)
            {
                throw new ArgumentException($"Address `{addressToAdd}` in town `{town}` already exist!");
            }

            Address newAddress = new Address
            {
                CreatedOn = DateTime.Now,
                ModifiedOn=DateTime.Now,
                Text = addressToAdd,
                Town = inTown,
                TownId = inTown.Id
            };

            inTown.Addresses.Add(newAddress);
            this.context.SaveChanges();

            return newAddress;
        }

        public Address EditText(string town, string addressToEdit)
        {
            Town inTown = this.context.Towns.FirstOrDefault(t => t.Name == town);
            if (inTown == null || inTown.IsDeleted)
            {
                throw new ArgumentException($"Town `{town}` doesn't exist!");
            }

            Address address = inTown.Addresses.FirstOrDefault(a => a.Text == addressToEdit);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToEdit}` in town `{town}` doesn't exist!");
            }

            address.Text = addressToEdit;
            address.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return address;
        }

        public Address EditTown(string oldTown, string addressToEdit, string newTown)
        {
            Town inTown = this.context.Towns.FirstOrDefault(t => t.Name == oldTown);
            if (inTown == null || inTown.IsDeleted)
            {
                throw new ArgumentException($"Town `{oldTown}` doesn't exist!");
            }

            Address address = inTown.Addresses.FirstOrDefault(a => a.Text == addressToEdit);

            if (address == null || address.IsDeleted)
            {
                throw new ArgumentException($"Address `{addressToEdit}` in town `{oldTown}` doesn't exist!");
            }

            Town inNewTown = this.context.Towns.FirstOrDefault(t => t.Name == newTown);
            if (inNewTown == null || inNewTown.IsDeleted)
            {
                throw new ArgumentException($"Town `{newTown}` doesn't exist!");
            }

            address.Town = inNewTown;
            address.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return address;
        }

        public Address Delete(string town, string addressToDelete)
        {
            Town inTown = this.context.Towns.FirstOrDefault(t => t.Name == town);
            if (inTown == null || inTown.IsDeleted)
            {
                throw new ArgumentException($"Town `{town}` doesn't exist!");
            }

            Address address = inTown.Addresses.FirstOrDefault(a => a.Text == addressToDelete);

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
