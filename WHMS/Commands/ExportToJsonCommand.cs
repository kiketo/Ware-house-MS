using System;
using System.Collections.Generic;
using System.IO;
using WHMS.Commands.Contracts;
using WHMS.Services;
using WHMS.Services.DatabaseServices;
using WHMSData.Models;

namespace WHMS.Commands
{
    public class ExportToJsonCommand : ICommand
    {
        private IJSONExportService databaseService;

        public ExportToJsonCommand(IJSONExportService databaseService)
        {
            this.databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            string jsonFileName;
            string jsonPath = @"./../../../../../DatabaseArchiveInJSON/";

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
            (new JSONExplorer<Address>()).Write(addressesList, jsonPath, jsonFileName);

            jsonFileName = "Categories.json";
            var categoriesList = this.databaseService.GetCategories();
            (new JSONExplorer<Category>()).Write(categoriesList, jsonPath, jsonFileName);

            jsonFileName = "Orders.json";
            var ordersList = this.databaseService.GetOrders();
            (new JSONExplorer<Order>()).Write(ordersList, jsonPath, jsonFileName);

            jsonFileName = "Partners.json";
            var partnersList = this.databaseService.GetPartners();
            (new JSONExplorer<Partner>()).Write(partnersList, jsonPath, jsonFileName);

            jsonFileName = "Products.json";
            var productList = this.databaseService.GetProducts();
            (new JSONExplorer<Product>()).Write(productList, jsonPath, jsonFileName);

            jsonFileName = "Towns.json";
            var townsList = this.databaseService.GetTowns();
            (new JSONExplorer<Town>()).Write(townsList, jsonPath, jsonFileName);

            jsonFileName = "Units.json";
            var unitsList = this.databaseService.GetUnits();
            (new JSONExplorer<Unit>()).Write(unitsList, jsonPath, jsonFileName);

            jsonFileName = "Warehouse.json";
            var warehousesList = this.databaseService.GetWarehouses();
            (new JSONExplorer<Warehouse>()).Write(warehousesList, jsonPath, jsonFileName);

            jsonFileName = "productWarehouses.json";
            var productWarehouses = this.databaseService.GetProductWarehouses();
            (new JSONExplorer<Warehouse>()).Write(warehousesList, jsonPath, jsonFileName);

            return "Data transfered to JSON";
        }
    }
}
