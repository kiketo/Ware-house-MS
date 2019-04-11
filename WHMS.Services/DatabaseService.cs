using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;
using System.IO;
using WHMSData.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WHMS.Services.Contracts;

namespace WHMS.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly WHMSContext context;
        ITownService townServices;
        IAddressSevice addressSevice;
        public DatabaseService(WHMSContext context, ITownService townServices, IAddressSevice addressSevice)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.townServices = townServices;
            this.addressSevice = addressSevice;
        }

        public List<Address> GetAddresses()
        {
            return this.context.Addresses.ToList();
        }
        public List<Category> GetCategories()
        {
            return this.context.Categories.ToList();
        }
        public List<Order> GetOrders()
        {
            return this.context.Orders.ToList();
        }
        public List<Partner> GetPartners()
        {
            return this.context.Partners.ToList();
        }
        public List<Product> GetProducts()
        {
            return this.context.Products.ToList();
        }
        public List<Town> GetTowns()
        {
            return this.context.Towns.ToList();
        }
        public List<Unit> GetUnits()
        {
            return this.context.Units.ToList();
        }
        public List<Warehouse> GetWarehouses()
        {
            return this.context.Warehouses.ToList();
        }

        public void PushAddresses(List<Address> addresses)
        {
            context.Set<Address>().AddRange(addresses.Where(a => !context.Addresses.Any(n => n.Text == a.Text)));
            //foreach (var address in addresses)
            //{
            //    this.context.Addresses.Add(new Address
            //    {
            //        //Id = address.Id,
            //        CreatedOn = address.CreatedOn,
            //        IsDeleted = address.IsDeleted,
            //        ModifiedOn = address.ModifiedOn,
            //        Text = address.Text,
            //        //TownId = address.TownId
            //    });
            //}
            this.context.SaveChanges();
        }
        public void PushTowns(List<Town> towns)
        {
            //context.Set<Town>().AddRange(towns.Where(a => !context.Towns.Any(n => n.Name == a.Name)));
            //foreach (var address in addresses)
            //{
            //    this.context.Addresses.Add(new Address
            //    {
            //        //Id = address.Id,
            //        CreatedOn = address.CreatedOn,
            //        IsDeleted = address.IsDeleted,
            //        ModifiedOn = address.ModifiedOn,
            //        Text = address.Text,
            //        //TownId = address.TownId
            //    });
            //}

            foreach (var town in towns)
            {
                this.townServices.Add(town.Name);
            }

            this.context.SaveChanges();
        }
        public void PushAddress(List<Address> addresses)
        {
            //context.Set<Town>().AddRange(towns.Where(a => !context.Towns.Any(n => n.Name == a.Name)));
            //foreach (var address in addresses)
            //{
            //    this.context.Addresses.Add(new Address
            //    {
            //        //Id = address.Id,
            //        CreatedOn = address.CreatedOn,
            //        IsDeleted = address.IsDeleted,
            //        ModifiedOn = address.ModifiedOn,
            //        Text = address.Text,
            //        //TownId = address.TownId
            //    });
            //}

            foreach (var a in addresses)
            {

               // this.addressSevice.Add(a.Town, a.Text);
            }

            this.context.SaveChanges();
        }
    }
}
