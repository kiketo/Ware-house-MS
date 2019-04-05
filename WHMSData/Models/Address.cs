using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Address : Model
    {
        [Required(ErrorMessage = "Text is required!")]
        public string Text { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }
    }
}