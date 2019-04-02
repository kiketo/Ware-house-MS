using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Town : Model
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}