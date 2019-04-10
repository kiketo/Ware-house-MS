using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Category : Model
    {
        [Required(ErrorMessage = "Category Name is required!")]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
