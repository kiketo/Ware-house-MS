using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;
using WHMSWebApp2.Models;

namespace WHMSWebApp2.Mappers
{
    public class ProductViewModelMapper : IViewModelMapper<Product, ProductViewModel>
    {
        public ProductViewModel MapFrom(Product entity)
        => new ProductViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            BuyPrice = entity.BuyPrice,
            SellPrice = entity.SellPrice,
            MarginInPercent = entity.MarginInPercent,
            Category = entity.Category?.Name,
            Description = entity.Description,
            Unit = entity.Unit?.UnitName,
            UnitId=entity.Unit?.Id,
            Creator=entity.Creator.UserName,
            CreatedOn = entity.CreatedOn,
            ModifiedOn = entity.ModifiedOn,
            CreatorId = entity.CreatorId
        };
    }
}
