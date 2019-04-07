using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreateWarehouseCommand :ICommand
    {
        IWarehouseService warehouseService;
        public CreateWarehouseCommand(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public string Execute(IReadOnlyList<string> parameters) // TODO Add address option
        {
            var name = parameters[0];
            var warehouse = this.warehouseService.CreateWarehouse(name);
            return $"Warehouse {warehouse.Name} was created";
        }
    }
}
