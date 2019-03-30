using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    class Product : Model
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public UnitType Unit { get; set; }

        public CategoryType Category { get; set; }

        public IList<Partner> PartnersList { get; set; }

        public decimal BuyPrice { get; set; }

        public int MarginInPercent { get; set; }

        public IList<WarehouseID> WarehousesList { get; set; }
    }
}
