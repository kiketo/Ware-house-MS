using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSWebApp.Models
{
    public class SearchOrderViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Please provide at least 3 letters")]
        public int GetOrderById { get; set; }

        public IReadOnlyCollection<OrderViewModel> SearchResults { get; set; } = new List<OrderViewModel>();
    }
}
