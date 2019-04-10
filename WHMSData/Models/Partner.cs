using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Partner : Model
    {
        [Required(ErrorMessage = "Partner Name is required!")]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }

        [JsonIgnore]
        public  Address Address { get; set; }

        [StringLength(11)]
        public string VAT { get; set; }

        [JsonIgnore]
        public ICollection<Order> PastOrders { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}