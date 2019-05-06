using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp2.Models
{
    public class AddViewModel
    {

        public IOrderedEnumerable<SelectListItem> Warehouses { get; set; }

        public Warehouse WareHouse { get; set; }

    }
}
