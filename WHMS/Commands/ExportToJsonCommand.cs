using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using Newtonsoft.Json;
using WHMS.Services;

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
            databaseService.ExportToJson();


            return "Data transfered to JSON";
        }
    }
}
