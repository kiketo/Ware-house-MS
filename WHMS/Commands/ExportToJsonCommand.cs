using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using Newtonsoft.Json;
using WHMS.Services;
using WHMSData.Models;
using WHMS.Services.DatabaseServices;
using System.IO;

namespace WHMS.Commands
{
    public class ExportToJsonCommand : ICommand
    {
        IDatabaseService databaseService;

        public ExportToJsonCommand(IDatabaseService databaseService)
        {
            this.databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            
            string jsonFileName;
            string jsonPath = "..\\..\\..\\..\\..\\DatabaseArchiveInJSON";
            if (!Directory.Exists(jsonPath))
            {
                try
                {
                    Directory.CreateDirectory(jsonPath);
                }
                catch (IOException)
                {
                    return $"Path does not exist!";
                }
            }
            jsonFileName = "Addresses.json";
            var addressesList = this.databaseService.GetAddresses();
            (new DatabaseJSON<Address>()).Write(addressesList, jsonPath, jsonFileName);

            jsonFileName = "Categories.json";
            var categoriesList = this.databaseService.GetCategories();
            (new DatabaseJSON<Category>()).Write(categoriesList, jsonPath, jsonFileName);

            jsonFileName = "Orders.json";
            var ordersList = this.databaseService.GetOrders();
            (new DatabaseJSON<Order>()).Write(ordersList, jsonPath, jsonFileName);

            jsonFileName = "Partners.json";
            var partnersList = this.databaseService.GetPartners();
            (new DatabaseJSON<Partner>()).Write(partnersList, jsonPath, jsonFileName);

            jsonFileName = "Products.json";
            var productList = this.databaseService.GetProducts();
            (new DatabaseJSON<Product>()).Write(productList, jsonPath, jsonFileName);

            jsonFileName = "Towns.json";
            var townsList = this.databaseService.GetTowns();
            (new DatabaseJSON<Town>()).Write(townsList, jsonPath, jsonFileName);

            jsonFileName = "Units.json";
            var unitsList = this.databaseService.GetUnits();
            (new DatabaseJSON<Unit>()).Write(unitsList, jsonPath, jsonFileName);

            jsonFileName = "Warehouse.json";
            var warehousesList = this.databaseService.GetWarehouses();
            (new DatabaseJSON<Warehouse>()).Write(warehousesList, jsonPath, jsonFileName);

            return "Data transfered to JSON";
        }
    }
}
