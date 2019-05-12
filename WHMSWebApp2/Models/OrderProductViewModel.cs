using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp2.Models
{
    public class OrderProductViewModel
    {
        public int InStock { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int WantedQuantity { get; set; }
    }
}
