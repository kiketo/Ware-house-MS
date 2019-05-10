using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WHMSData.Utills;

namespace WHMSData.Models
{
    public class OrderProductWarehouse 
    {
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int WarehouseId { get; set; }

        public int WantedQuantity { get; set; }

    }
}