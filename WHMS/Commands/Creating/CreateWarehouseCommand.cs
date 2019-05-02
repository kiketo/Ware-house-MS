using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreateWarehouseCommand :ICommand
    {
        IWarehouseService warehouseService;
        ITownService townService;
        IAddressSevice addressService;
        public CreateWarehouseCommand(IWarehouseService warehouseService, ITownService townService, IAddressSevice addressService)
        {
            this.warehouseService = warehouseService;
            this.townService = townService;
            this.addressService = addressService;
        }

        public Task<string> Execute(IReadOnlyList<string> parameters) 
        {
            //var name = parameters[0];
            //var city = this.townService.GetTown(parameters[1]);
            //var addressText = parameters[2];
            //var address = this.addressService.GetAddress(city, addressText);
            //var warehouse = this.warehouseService.CreateWarehouse(name,address );
            //return $"Warehouse {warehouse.Name} was created";
            throw new NotImplementedException();//TODO
        }
    }
}
