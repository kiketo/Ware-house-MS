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
        [Range(1,int.MaxValue,ErrorMessage ="ID should be non negative number")]
        public int GetOrderById { get; set; }

        public IReadOnlyCollection<OrderViewModel> SearchResults { get; set; } = new List<OrderViewModel>();
    }
}
