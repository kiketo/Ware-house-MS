using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;
using WHMSData.Utills;

namespace WHMSData.Models
{
    public class Order : Model
    {
        [Required(ErrorMessage = "OrderType is required!")]
        public OrderType Type { get; set; }

        [JsonIgnore]
        public int PartnerId { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Order Partner is required!")]
        public Partner Partner { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Order Products is required!")]
        public ICollection<Product> Products { get; set; }

        public string Comment { get; set; }

        public decimal TotalValue { get; set; }
    }
}
