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
            if (this.context.Towns.FirstOrDefault(t => t.Name == townToAddName) != null)
            {
                throw new ArgumentException($"Town `{townToAddName}` already exist!");
            }

            Town townToAdd = new Town()
            {
                Addresses = new List<Address>(),
                CreatedOn = DateTime.Now,
                ModifiedOn=DateTime.Now,
                Name = townToAddName
            };

            this.context.Towns.Add(townToAdd);
            this.context.SaveChanges();

            return townToAdd;
        }

        public Town Edit(string townToEditName)
        {
            Town townToEdit = this.context.Towns
                .FirstOrDefault(t => t.Name == townToEditName);

            if (townToEdit == null || townToEdit.IsDeleted)
            {
                throw new ArgumentException($"Town `{townToEditName}` doesn't exist!");
            }

            townToEdit.Name = townToEditName;
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
                .Include(a=>a.Addresses)
                .FirstOrDefault(t => t.Name == townToGetName);

            if (townToGet == null || townToGet.IsDeleted)
            {
                throw new ArgumentException($"Town `{townToGetName}` doesn't exist!");
            }
            
            return townToGet;
        }
    }
}
