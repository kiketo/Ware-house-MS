using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp.Models
{
    public class PartnerViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "ID should be a positive number")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Partner Name is required!")]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }

        [StringLength(11)]
        public string VAT { get; set; }

        public PartnerViewModel SearchResult { get; set; }
    }
}
