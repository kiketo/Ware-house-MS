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
    public class JSONService : IJSONExportService, IJSONImportService
    {
        private readonly WHMSContext context;
        ITownService townServices;
        IAddressSevice addressSevice;
        public JSONService(WHMSContext context, ITownService townServices, IAddressSevice addressSevice)
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
        public List<ProductWarehouse> GetProductWarehouses()
        {
            return this.context.ProductWarehouse.ToList();
        }

        public void ImportTowns(Town[] towns)
        {
            if (towns.Length == 0)
            {
                throw new ArgumentException("No towns to import");
            }

            foreach (Town t in towns)
            {
                this.context.Towns.Add(t);
            }

            this.context.SaveChanges();
        }
    }
}
