using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;
using WHMSWebApp2.Models;
using WHMSWebApp2.Models.OrderViewModels;

namespace WHMSWebApp2.Mappers
{
    public class OrderViewModelMapper : IViewModelMapper<Order, OrderViewModel>
    {
        public OrderViewModel MapFrom(Order entity)
        => new OrderViewModel
        {
            Id=entity.Id,
            Partner=entity.Partner.Name,
            Comment=entity.Comment,
            CreatedOn=entity.CreatedOn,
            Creator=entity.Creator,
            CreatorId=entity.CreatorId,
            IsDeleted=entity.IsDeleted,
            ModifiedOn=entity.ModifiedOn,
            PartnerId=entity.PartnerId,//TODO
            //Products=entity.Products,
            TotalValue=entity.TotalValue,
            Type=entity.Type.ToString(),
            TypeOrder= entity.Type
            
        };
    }
}
