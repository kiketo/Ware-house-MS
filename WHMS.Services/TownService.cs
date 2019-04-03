using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WHMSData.Context;
using WHMSData.Models;
using WHMS.Services.Interfaces;

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
            if (this.context.Towns.FirstOrDefault(t=>t.Name==townToAddName)!=null)
            {
                throw new ArgumentException($"Town `{townToAddName}` already exist!");
            }

            Town townToAdd = new Town()
            {
                Addresses = new List<Address>(),
                CreatedOn = DateTime.Now,                
                Name=townToAddName
            };

            context.Towns.Add(townToAdd);
            context.SaveChanges();

            return townToAdd;
        }
        public Town Edit (string townToEditName)
        {
            Town townToEdit = context.Towns
                .FirstOrDefault(t => t.Name == townToEditName);

            if (townToEdit==null||townToEdit.IsDeleted)
            {
                throw new ArgumentException($"Town `{townToEditName}` doesn't exist!");
            }
                        
            townToEdit.Name = townToEditName;
            townToEdit.ModifiedOn = DateTime.Now;

            context.Towns.Update(townToEdit);
            context.SaveChanges();

            return townToEdit;
        }
        public void Delete (string townToDeleteName)
        {
            Town townToDelete=context.Towns
                .FirstOrDefault(t => t.Name == townToDeleteName);

            if (townToDelete == null|| townToDelete.IsDeleted)
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

            context.Towns.Update(townToDelete);
            context.SaveChanges();
        }
    }
}
