using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp.Models;

namespace WHMSWebApp.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;
        IUnitService unitService;
        ICategoryService categoryService;

        public ProductController(
            IProductService productService, 
            IUnitService unitService, 
            ICategoryService categoryService
            )
        {
            this.productService = productService;
            this.unitService = unitService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Unit"] = new SelectList( unitService.GetAllUnits(), "Id", "UnitName").OrderBy(x => x.Text);
            ViewData["Category"] = new SelectList(categoryService.GetAllCategories(), "Id", "Name").OrderBy(x => x.Text);
            return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                
                var newProduct = this.productService.CreateProduct(
                    product.Name,
                    unitService.GetUnitByID(int.Parse(product.Unit)),
                    categoryService.FindByID(int.Parse(product.Category)),
                    product.BuyPrice,
                    product.MarginInPercent,
                    product.Description
                    );

                return RedirectToAction(nameof(Details), new { id = newProduct.Id });
            }
            else
            {
                return View();
            }
        }
        public IActionResult Details(int id)
        {
            var model = this.productService.GetProductById(id);
            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }


    }
}
