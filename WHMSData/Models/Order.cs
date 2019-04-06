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

        public int PartnerId { get; set; }

        [Required(ErrorMessage = "Order Partner is required!")]
        public Partner Partner { get; set; }

        [Required(ErrorMessage = "Order Products is required!")]
        public ICollection<Product> Products { get; set; }

        public string Comment { get; set; }

        public decimal TotalValue { get; set; }
    }
}
