﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp.Models;
using WHMSWebApp.Models.OrderViewModels;

namespace WHMSWebApp.Controllers
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

        public IActionResult Search()
        {
            return View();
        }
        
        public async Task<IActionResult> Add()
        {
            OrderViewModel model = new OrderViewModel()
            {
                Warehouses = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "Id", "Name").OrderBy(x => x.Text)
        };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(OrderViewModel model)
        {
            model.Warehouses = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "Id", "Name").OrderBy(x => x.Text);
            var warehouse = await this.warehouseService.GetByNameAsync(model.Warehouse);
                    


            return RedirectToAction("Create", "Order", new { id = model.Id });

    
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
