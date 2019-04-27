﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;
using WHMSWebApp.Models;

namespace WHMSWebApp.Mappers
{
    public class OrderViewModelMapper : IViewModelMapper<Order, OrderViewModel>
    {
        public OrderViewModel MapFrom(Order entity)
        => new OrderViewModel
        {
            Id=entity.Id,
            Partner=entity.Partner,
            Comment=entity.Comment,
            CreatedOn=entity.CreatedOn,
            Creator=entity.Creator,
            CreatorId=entity.CreatorId,
            IsDeleted=entity.IsDeleted,
            ModifiedOn=entity.ModifiedOn,
            PartnerId=entity.PartnerId,
            Products=entity.Products,
            TotalValue=entity.TotalValue,
            Type=entity.Type
        };
    }
}