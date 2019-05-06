using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp.Models
{
    public class WarehouseViewModel
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "Warehouse Name is required!")]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }


        public int AddressId { get; set; }


        public Address Address { get; set; }


        public ICollection<ProductWarehouse> Products { get; set; }



    }
}
