using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;
using System.IO;
using WHMSData.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WHMS.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly WHMSContext context;

        public DatabaseService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
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
           
            foreach (var address in addresses)
            {
                this.context.Addresses.Add(new Address
                {
                    //Id = address.Id,
                    CreatedOn = address.CreatedOn,
                    IsDeleted = address.IsDeleted,
                    ModifiedOn = address.ModifiedOn,
                    Text = address.Text,
                    //TownId = address.TownId
                });
            }
            this.context.SaveChanges();
        }
    }
}
