using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp2.Models
{
    public class ProductViewModel
    {
       // [Range(1, int.MaxValue, ErrorMessage = "ID should be a positive number")]
        public int Id { get; set; }

        [StringLength(30,ErrorMessage = "Product Name must be between 4 and 30 letters!", MinimumLength =4)]
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

        public IReadOnlyCollection<ProductViewModel> SearchResults { get; set; } = new List<ProductViewModel>();

        public IOrderedEnumerable<SelectListItem> Categories { get; set; }

        public IOrderedEnumerable<SelectListItem> Units { get; set; }
        
    }
}
