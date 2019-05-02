using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;
using WHMSData.Models;

namespace WHMS.Commands
{
    public class ImportTownsFromJsonCommand : ICommand
    {
        private IJSONImportService dbService;

        public ImportTownsFromJsonCommand(IJSONImportService dbService)
        {
            this.dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        public Task<string> Execute(IReadOnlyList<string> parameters)
        {
            //string jsonFileName;
            //string jsonPath = @"./../../../../../DatabaseArchiveInJSON/";

            //jsonFileName = "Towns.json";
            //var towns = File.ReadAllText($"{jsonPath}\\{jsonFileName}");
            //var townsJson = JsonConvert.DeserializeObject<Town[]>(towns);
            //dbService.ImportTowns(townsJson);

            //return "Successfully Imported Towns!";
            throw new NotImplementedException();//TODO
        }
    }
}