using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class UnitService : IUnitService
    {
        private readonly ApplicationDbContext context;

        public UnitService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> CreateUnitAsync(string name)
        {
            var newUnit = await this.context.Units.FirstOrDefaultAsync(t => t.UnitName == name);
            if (newUnit!=null)
            {
                return newUnit;
            }

            newUnit = new Unit()
            {
                UnitName = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
                
            };
            await this.context.Units.AddAsync(newUnit);
            await this.context.SaveChangesAsync();

            return newUnit;
        }

        public  Task<List<Unit>> GetAllUnitsAsync()
        {
            return this.context.Units.Where(u => u.IsDeleted == false).ToListAsync();
        }

        public async Task<Unit> GetUnitAsync(string name)
        {
            var unit = await this.context.Units
                .Where(u => u.UnitName == name)
                .FirstOrDefaultAsync();

            if (unit == null || unit.IsDeleted)
            {
                throw new ArgumentException($"Unit with name {name} does not exist");
            }
            return unit;
        }

        public Task<Unit> GetUnitByIDAsync(int? id)
        {
            var unit = this.context.Units.Where(u => u.Id == id).FirstOrDefaultAsync();
           
            return unit;
        }

        //public async Task<Unit> DeleteUnitNameAsync(int unitId)
        //{
        //    var unitToDel = await this.context.Units.FirstOrDefaultAsync(t => t.Id == unitId);
        //    if (unitToDel == null || unitToDel.IsDeleted)
        //    {
        //        throw new ArgumentException($"Such unit does not exists");
        //    }
        //    unitToDel.IsDeleted = true;
        //    unitToDel.ModifiedOn = DateTime.Now;

        //    await this.context.SaveChangesAsync();
        //    return unitToDel;
        //}

        //public async Task<Unit> ModifyUnitNameAsync(string name)
        //{
        //    var unitToMod = await this.context.Units.Where(t => t.UnitName == name).FirstOrDefaultAsync();
        //    if (unitToMod == null || unitToMod.IsDeleted)
        //    {
        //        throw new ArgumentException($"Unit {name} does not exists");
        //    }
        //    unitToMod.UnitName = name;
        //    unitToMod.ModifiedOn = DateTime.Now;

        //    this.context.SaveChanges();
        //    return unitToMod;
        //}
    }
}
