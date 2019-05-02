using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public Task<string> Execute(IReadOnlyList<string> parameters)
        {
            //if (parameters.Count!=1)
            //{
            //    throw new ArgumentException(@"Please provide parameter: Unit");
            //}

            //var unit = unitService.CreateUnit(parameters[0]);
            //return $"Unit {unit.UnitName} was created.";
            throw new NotImplementedException();//TODO
        }

    }
}
