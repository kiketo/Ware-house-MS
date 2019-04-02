using System.Collections.Generic;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Warehouse : Model
    {
        public string Name { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<ProductWarehouse> Products { get; set; }

        public ICollection<ProductOrderWarehouse> ProductsAndOrders { get; set; }
    }
}