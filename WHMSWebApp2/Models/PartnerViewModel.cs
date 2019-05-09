using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp2.Models
{
    public class PartnerViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "ID should be a positive number")]
        public int Id { get; set; }

        [MinLength(4, ErrorMessage = "Partner Name is required!")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }

        [StringLength(11)]
        [MinLength(11,ErrorMessage="Please enter a valid VAT number (ex: BG123456789)")]
        public string VAT { get; set; }

        public string CreatorId { get; set; }

        public string Creator { get; set; }

        public bool CanUserEdit { get; set; }

        public bool CanUserDelete { get; set; }

        public PartnerViewModel SearchResult { get; set; }

        public IOrderedEnumerable<SelectListItem> Cities { get; set; }

        public bool ConfirmationToDelete { get; set; }

        public IOrderedEnumerable<SelectListItem> Addresses { get; set; }

        public List<Address> AddressesList { get; set; }

        public int AddressId { get; set; }

    }
}
