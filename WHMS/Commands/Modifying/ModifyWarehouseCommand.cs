using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Modifying
{
    public class ModifyWarehouseCommand : ICommand
    {
        IWarehouseService warehouseService;
        public ModifyWarehouseCommand(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }
        public string Execute(IReadOnlyList<string> parameters) //oldname;newname
        {
            this.warehouseService.ModifyWarehouseName(parameters[0],parameters[1]);
            return $"The warehouse name{parameters[0]} was changed to {parameters[1]}";
        }
    }
}
