using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Partner : Model
    {
        [Required]
        public string Name { get; set; }

        public  Address Address { get; set; }

        public decimal VAT { get; set; }

        //public ICollection<Order> PastOrders { get; set; }

    }
}