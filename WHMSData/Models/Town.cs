using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Town : Model
    {
        [Required(ErrorMessage = "Town Name is required!")]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Address> Addresses { get; set; }
    }
}