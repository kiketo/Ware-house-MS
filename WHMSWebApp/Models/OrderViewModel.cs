using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMSWebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "OrderType is required!")]
        public OrderType Type { get; set; }

        public int PartnerId { get; set; }

        [Required(ErrorMessage = "Order Partner is required!")]
        public string Partner { get; set; }

        [Required(ErrorMessage = "Order Products is required!")]
        public ICollection<Product> Products { get; set; }

        public string Comment { get; set; }

        public decimal TotalValue { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}