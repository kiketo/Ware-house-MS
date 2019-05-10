using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Warehouse : Model
    {
        [Required(ErrorMessage = "Warehouse Name is required!")]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }

        [JsonIgnore]
        public int AddressId { get; set; }

        [JsonIgnore]
        public Address Address { get; set; }

        [JsonIgnore]
        public ICollection<ProductWarehouse> Products { get; set; }

        [JsonIgnore]
        public ICollection<OrderProductWarehouse> OrderProductWarehouses { get; set; }
    }
}