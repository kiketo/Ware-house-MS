using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Product : Model
    {        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int UnitId { get; set; }

        public Unit Unit { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public decimal BuyPrice { get; set; }

        public double MarginInPercent { get; set; }

        public decimal SellPrice { get; set; }

        public ICollection<ProductWarehouse> Warehouses { get; set; }
    }
}
