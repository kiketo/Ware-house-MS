﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp2.Models;
using WHMSWebApp2.Models.OrderViewModels;

namespace WHMSWebApp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWarehouseService warehouseService;

        public HomeController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
               
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

    }
}
