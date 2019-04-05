using System;
using System.Collections.Generic;
using System.Text;

namespace WHMSData.Models
{
    public class ProductOrderWarehouse
    {
        public ICollection <ProductWarehouse> productsInWarehouse { get; set; }

        public int MyProperty { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int OrderQuantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}
