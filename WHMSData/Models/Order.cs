using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Order : Model
    {
        [Required]
        public int PartnerID { get; set; }
        public Partner Partner { get; set; }

        public List<Product> Products { get; set; }

        

        //public string Comment { get; set; } // can be comments

        //public int WarehouseID { get; set; }
        //public Warehouse Warehouse { get; set; }

        ////total value of products
        //In/Out Enum or bool
    }
}
