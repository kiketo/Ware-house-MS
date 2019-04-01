using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Order : Model
    {
        

        public int PartnerId { get; set; }
        public Partner Partner { get; set; }

        public List<Product> Products { get; set; }



        public string Comment { get; set; } // can be comments

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        ////total value of products
        //In/Out Enum or bool
    }
}
