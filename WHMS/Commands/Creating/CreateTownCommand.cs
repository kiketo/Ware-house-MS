using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreateTownCommand : ICommand
    {
        private readonly ITownService townService;

        public CreateTownCommand(ITownService townService)
        {
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }

        //createtown;Sofia
        public Task<string> Execute(IReadOnlyList<string> parameters)
        {
            //if (parameters.Count != 1)
            //{
            //    throw new ArgumentException(@"Please provide parameter: Town");
            //}

            //var town = this.townService.Add(parameters[0]);
            //return $"Town {town.Name} was created.";
            throw new NotImplementedException();//TODO
        }
    }
}
