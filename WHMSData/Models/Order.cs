using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
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

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalValue { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}
