using System.Collections.Generic;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Transfer : Model
    {
        public Warehouse FromWarehouse { get; set; }

        public Warehouse ToWarehouse { get; set; }

        public Dictionary<Product,int> QtyPerProduct { get; set; }

        //public ICollection<Product> Products { get; set; }

        public string Comments { get; set; }
    }
}
