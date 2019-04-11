using Newtonsoft.Json;
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
    public class ImportTownsFromJsonCommand : ICommand
    {
        IJSONImportService dbService;

        public ImportTownsFromJsonCommand(IJSONImportService dbService)
        {
            this.dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            string jsonFileName;
            string jsonPath = "..\\..\\..\\..\\WHMSData\\DatabaseArchiveInJSON";



            jsonFileName = "Towns.json";
            var towns = File.ReadAllText($"{jsonPath}\\{jsonFileName}");
            var townsJson = JsonConvert.DeserializeObject<Town[]>(towns);
            dbService.ImportTowns(townsJson);

            return "Successfully Imported Towns!";

        }
    }
}