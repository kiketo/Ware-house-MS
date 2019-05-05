using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class TownService : ITownService
    {
        private readonly ApplicationDbContext context;

        public TownService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Town> Add(string townToAddName)
        {
            Town townToAdd = await this.context.Towns.FirstOrDefaultAsync(t => t.Name == townToAddName);

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
               await this.context.Towns.AddAsync(townToAdd);
            }

            await this.context.SaveChangesAsync();

            return townToAdd;
        }

        public async Task<Town> Edit(string oldTownName, string newTownName)
        {
            Town townToEdit = await this.context.Towns
                .FirstOrDefaultAsync(t => t.Name == oldTownName);

            if (townToEdit == null || townToEdit.IsDeleted)
            {
                throw new ArgumentException($"Town `{oldTownName}` doesn't exist!");
            }

            townToEdit.Name = newTownName;
            townToEdit.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();

            return townToEdit;
        }

        public async Task<Town> Delete(string townToDeleteName)
        {
            Town townToDelete = await this.context.Towns
                .FirstOrDefaultAsync(t => t.Name == townToDeleteName);

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

            await this.context.SaveChangesAsync();
            return townToDelete;
        }

        public async Task<Town> GetTown(string townToGetName)
        {
            Town townToGet = await this.context.Towns
                .Include(a => a.Addresses)
                .FirstOrDefaultAsync(t => t.Name == townToGetName);

            if (townToGet == null || townToGet.IsDeleted)
            {
                throw new ArgumentException($"Town `{townToGetName}` doesn't exist!");
            }

            return townToGet;
        }
        public async Task<IEnumerable<Town>> GetAllTowns()
        {
            var allTowns = await this.context.Towns
                .Include(a => a.Addresses)
                .ToListAsync();

            
            return allTowns;
        }
        public async Task<Town> GetTownById(int id)
        {
            Town townToGet = await this.context.Towns
                .Include(a => a.Addresses)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (townToGet == null || townToGet.IsDeleted)
            {
                throw new ArgumentException($"Town  doesn't exist!");
            }

            return townToGet;
        }
    }
}
