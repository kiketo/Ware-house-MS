using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Address : Model
    {
        [Required(ErrorMessage = "Address Text is required!")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Text { get; set; }

        [JsonIgnore]
        public int TownId { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Address Town is required!")]
        public Town Town { get; set; }
    }
}