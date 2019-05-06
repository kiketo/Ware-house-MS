using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;
using WHMSWebApp2.Models;

namespace WHMSWebApp2.Mappers
{
    public class WarehouseViewModelMapper : IViewModelMapper<Warehouse, WarehouseViewModel>
    {
        public WarehouseViewModel MapFrom(Warehouse entity)
            => new WarehouseViewModel
        {
                Id = entity.Id,
                 Name = entity.Name,
                 Products = entity.Products,
                 Address = entity.Address,
                 AddressId = entity.AddressId
        };
    }
}

