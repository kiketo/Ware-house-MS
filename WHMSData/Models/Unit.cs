using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Unit : Model
    {
        [Required(ErrorMessage = "UnitName is required!")]
        [MinLength(1)]
        [MaxLength(10)]
        public string UnitName { get; set; }
    }
}
