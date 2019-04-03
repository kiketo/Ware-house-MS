using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class UnitService 
    {
        private readonly WHMSContext context;

        public UnitService(WHMSContext context)
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
                throw new ArgumentException($"Category {name} does not exists");
            }
            unitToMod.UnitName = name;
            unitToMod.ModifiedOn = DateTime.Now;

            this.context.Units.Update(unitToMod);
            this.context.SaveChanges();
            return unitToMod;
        }
        
    }
}
