using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class TownService : ITownService
    {
        private readonly WHMSContext context;

        public TownService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Town Add(string townToAddName)
        {
            Town townToAdd = this.context.Towns.FirstOrDefault(t => t.Name == townToAddName);

            if (townToAdd != null)
            {
                if (townToAdd.IsDeleted)
                {
                    townToAdd.Addresses = new List<Address>();
                    townToAdd.CreatedOn = DateTime.Now;
                    townToAdd.ModifiedOn = DateTime.Now;
                    townToAdd.IsDeleted = false;
                    townToAdd.Name = townToAddName;
                }
                else
                {
                    throw new ArgumentException($"Town `{townToAddName}` already exist!");
                }
            }
            else
            {
                townToAdd = new Town()
                {
                    Addresses = new List<Address>(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsDeleted = false,
                    Name = townToAddName
                };
                this.context.Towns.Add(townToAdd);
            }

            this.context.SaveChanges();

            return townToAdd;
        }

        public Town Edit(string oldTownName, string newTownName)
        {
            Town townToEdit = this.context.Towns
                .FirstOrDefault(t => t.Name == oldTownName);

            if (townToEdit == null || townToEdit.IsDeleted)
            {
                throw new ArgumentException($"Town `{oldTownName}` doesn't exist!");
            }

            townToEdit.Name = newTownName;
            townToEdit.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();

            return townToEdit;
        }

        public Town Delete(string townToDeleteName)
        {
            Town townToDelete = this.context.Towns
                .FirstOrDefault(t => t.Name == townToDeleteName);

            if (townToDelete == null || townToDelete.IsDeleted)
            {
                throw new ArgumentException($"Town `{townToDeleteName}` doesn't exist!");
            }

            townToDelete.ModifiedOn = DateTime.Now;
            townToDelete.IsDeleted = true;
            foreach (var address in townToDelete.Addresses)
            {
                address.IsDeleted = true;
                address.ModifiedOn = DateTime.Now;
            }

            this.context.SaveChanges();
            return townToDelete;
        }

        public Town GetTown(string townToGetName)
        {
            Town townToGet = this.context.Towns
                .Include(a => a.Addresses)
                .FirstOrDefault(t => t.Name == townToGetName);

            if (townToGet == null || townToGet.IsDeleted)
            {
                throw new ArgumentException($"Town `{townToGetName}` doesn't exist!");
            }

            return townToGet;
        }
    }
}
