using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class JSONService : IJSONExportService, IJSONImportService
    {
        private readonly ApplicationDbContext context;
        private ITownService townServices;
        private IAddressSevice addressSevice;
        public JSONService(ApplicationDbContext context, ITownService townServices, IAddressSevice addressSevice)
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
