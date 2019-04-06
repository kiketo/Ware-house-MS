using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreateUnitCommand:ICommand
    {
        private IUnitService unitService;

        public CreateUnitCommand(IUnitService unitService)
        {
            this.unitService = unitService ?? throw new ArgumentNullException(nameof(unitService));
        }

        //createunit;kg
        public string Execute(IReadOnlyList<string> parameters)
        {
            if (parameters.Count==0)
            {
                throw new ArgumentException(@"Please provide parameters: {unit}");
            }

            throw new NotImplementedException();
        }

    }
}
