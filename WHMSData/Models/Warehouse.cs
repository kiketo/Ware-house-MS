using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Warehouse : Model
    {
        [Required]
        public string Name { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<ProductWarehouse> Products { get; set; }
    }
}