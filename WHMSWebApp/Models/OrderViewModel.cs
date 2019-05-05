using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMSWebApp.Models.OrderViewModels
{
    public class OrderViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "ID should be a positive number")]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string Type { get; set; }

        public int PartnerId { get; set; }

        [MinLength(0,ErrorMessage = "Partner is required!")]
        public string Partner { get; set; }

        [Required(ErrorMessage = "Product is required!")]
        public ICollection<Product> Products { get; set; } //TODO string

        public string Comment { get; set; }

        public decimal TotalValue { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public int Quantity { get; set; }

        public string Warehouse { get; set; }

        public IReadOnlyCollection<OrderViewModel> SearchResults { get; set; } = new List<OrderViewModel>();
    }
}