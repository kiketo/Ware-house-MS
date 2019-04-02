using System;
using System.Collections.Generic;
using System.Text;

namespace WHMSData.Models
{
    public class ProductOrderWarehouse
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int Quantity { get; set; }
    }
}
