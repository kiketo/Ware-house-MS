using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSWebApp.Controllers
{
    public class PartnerController : Controller
    {

        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            return View();
        }
     
        public IActionResult Details()
        {
            return View();
        }
        
        public IActionResult Edit()
        {
            return View();
        }

    }
}
