using System.Collections.Generic;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Category : Model
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
