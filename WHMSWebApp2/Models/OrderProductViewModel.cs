using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp2.Models
{
    public class OrderProductViewModel
    {
        public int id { get; set; }
        public int wantedQuantity { get; set; }
        public int inStock { get; set; }
        public Product product { get; set; }
    }
}
