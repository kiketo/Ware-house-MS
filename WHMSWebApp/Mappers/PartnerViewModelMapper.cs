﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;
using WHMSWebApp.Models;

namespace WHMSWebApp.Mappers
{
    public class PartnerViewModelMapper : IViewModelMapper<Partner, PartnerViewModel>
    {
        public PartnerViewModel MapFrom(Partner entity)
        => new PartnerViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            VAT = entity.VAT,
            City = entity.Address?.Town.Name,
            Address = entity.Address?.Text
        };
    }
}
