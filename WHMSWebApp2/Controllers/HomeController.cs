using Microsoft.AspNetCore.Mvc;
using System;
using WHMS.Services.Contracts;

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
