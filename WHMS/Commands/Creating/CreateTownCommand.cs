using System;
using System.Collections.Generic;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{

    //just for testing purposes
    public class CreateTownCommand : ICommand
    {
        private readonly ITownService townService;

        public CreateTownCommand(ITownService townService)
        {
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            this.townService.Add(parameters[0]);

            return $"Town with name {parameters[0]} added sucsefully";

        }
    }
}
