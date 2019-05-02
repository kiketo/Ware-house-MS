using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Utills;

namespace WHMSWebApp.Models
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "OrderType is required!")]
        public OrderType Type { get; set; }

        [Required(ErrorMessage = "Order Partner is required!"), MaxLength(30)]
        public string PartnerName { get; set; }

        [Required(ErrorMessage = "Order Products is required!")]
        public string Products { get; set; }

        public string Comment { get; set; }
    }
}
