using System.Collections.Generic;
using WHMSData.Contracts;
using WHMSData.Utills;

namespace WHMSData.Models
{
    public class Order : Model
    {
        public OrderType Type { get; set; }

        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        public ICollection<Product> Products { get; set; }

        public string Comment { get; set; }

        public decimal TotalValue { get; set; }
    }
}
