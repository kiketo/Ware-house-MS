using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp.Models
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Product Name is required!")]
        [MaxLength(30)]
        [MinLength(4)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Product Unit is required!")]
        public string Unit { get; set; }

        public string Category { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BuyPrice { get; set; }

        [Required(ErrorMessage = "Product Margin is required!")]
        public double MarginInPercent { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SellPrice { get; set; }
    }
}
