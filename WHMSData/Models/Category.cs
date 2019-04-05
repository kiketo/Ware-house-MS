using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Category : Model
    {
        [Required]
        public string Name { get; set; }

        //public ICollection<Product> Products { get; set; }
    }
}
