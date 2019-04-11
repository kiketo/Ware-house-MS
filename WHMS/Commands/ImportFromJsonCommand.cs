using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services;
using WHMS.Services.Contracts;
using WHMS.Services.DatabaseServices;
using WHMSData.Models;

namespace WHMS.Commands
{
    public class ImportFromJsonCommand : ICommand
    {
        IDatabaseService databaseService;
        
        public ImportFromJsonCommand(IDatabaseService databaseService, ITownService townServices)
        {
            this.databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            string jsonFileName;
            string jsonPath = "..\\..\\..\\..\\DatabaseArchiveInJSON";
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

            //jsonFileName = "Towns.json";

            //this.databaseService.PushTowns((new DatabaseJSON<Town>()).Read(jsonPath, jsonFileName));

            jsonFileName = "Addresses.json";

            this.databaseService.PushAddresses((new DatabaseJSON<Address>()).Read(jsonPath, jsonFileName));

            //jsonFileName = "Categories.json";
            //(new DatabaseJSON<Category>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Orders.json";
            //(new DatabaseJSON<Order>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Partners.json";
            //(new DatabaseJSON<Partner>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Products.json";
            //(new DatabaseJSON<Product>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Towns.json";
            ////(new DatabaseJSON<Town>()).Read(jsonPath, jsonFileName);

            //this.databaseService.PushTowns((new DatabaseJSON<Town>()).Read(jsonPath, jsonFileName));

            //jsonFileName = "Units.json";
            //(new DatabaseJSON<Unit>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Warehouse.json";
            //(new DatabaseJSON<Warehouse>()).Read(jsonPath, jsonFileName);

            return "Data transfered from JSON";
        }
    }
}