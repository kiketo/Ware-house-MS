using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Unit CreateUnit(string name)
        {
            if (this.context.Units.Any(t => t.UnitName == name))
            {
                throw new ArgumentException($"Unit {name} already exists");
            }

            var newUnit = new Unit()
            {
                UnitName = name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
                
            };
            this.context.Units.Add(newUnit);
            this.context.SaveChanges();

            return newUnit;
        }

        public Unit ModifyUnitName(string name)
        {
            var unitToMod = this.context.Units.FirstOrDefault(t => t.UnitName == name);
            if (unitToMod == null || unitToMod.IsDeleted)
            {
                throw new ArgumentException($"Unit {name} does not exists");
            }
            unitToMod.UnitName = name;
            unitToMod.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return unitToMod;
        }

        public List<Unit> GetAllUnits()
        {
            return this.context.Units.Where(u => u.IsDeleted != true).ToList();
        }

        public Unit DeleteUnitName(int unitId)
        {
            var unitToDel = this.context.Units.FirstOrDefault(t => t.Id == unitId);
            if (unitToDel == null || unitToDel.IsDeleted)
            {
                throw new ArgumentException($"Such unit does not exists");
            }
            unitToDel.IsDeleted = true;
            unitToDel.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return unitToDel;
        }
        public Unit GetUnit(string name)
        {
            var unit = this.context.Units.Where(u => u.UnitName == name).FirstOrDefault();
            if (unit == null || unit.IsDeleted)
            {
                throw new ArgumentException($"Unit with name {name} does not exist");
            }
            return unit;
        }

    }
}
