using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WHMSWebApp.Models;

namespace WHMSWebApp.Controllers
{
    public class AddController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreatePartner()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateProduct(ProductViewModel restaurantView)
        //{
        //    if (ModelState.IsValid)
        //    {
                
        //        return RedirectToAction(nameof(DetailsProduct), "Home", new { id = item.Id });
        //    }
        //    else
        //    {
        //        return View();
        //    }


        //}
    }
}