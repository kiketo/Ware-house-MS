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

        public int TownId { get; set; }

        [Required(ErrorMessage = "Address Town is required!")]
        public Town Town { get; set; }
    }
}