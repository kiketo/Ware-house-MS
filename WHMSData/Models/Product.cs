using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Product : Model
    {        
        [Required(ErrorMessage = "Product Name is required!")]
        [MaxLength(30)]
        [MinLength(4)]
        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public int UnitId { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Product Unit is required!")]
        public Unit Unit { get; set; }

        [JsonIgnore]
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BuyPrice { get; set; }

        [Required(ErrorMessage = "Product Margin is required!")]
        public double MarginInPercent { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SellPrice { get; set; }

        [JsonIgnore]
        public ICollection<ProductWarehouse> Warehouses { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public ICollection<OrderProductWarehouse> OrderProductWarehouses { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
