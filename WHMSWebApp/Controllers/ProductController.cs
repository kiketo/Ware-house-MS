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
            if (this.productService.GetProducts().Any(p=>p.Name == product.Name))
            {
                ModelState.AddModelError("Name", "The name must be unique.");
            }
            ViewData["Unit"] = new SelectList(unitService.GetAllUnits(), "Id", "UnitName").OrderBy(x => x.Text);
            ViewData["Category"] = new SelectList(categoryService.GetAllCategories(), "Id", "Name").OrderBy(x => x.Text);

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
                return View(product);
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

        //public async Task<IActionResult> SearchPartnerById([FromQuery]PartnerViewModel model)
        //{
        //    if (model.Id == 0)
        //    {
        //        return View();
        //    }
        //    try
        //    {
        //        model.SearchResults = new List<OrderViewModel>
        //    {
        //        this.orderMapper.MapFrom(await this.orderService.GetOrderByIdAsync(model.Id))
        //    };
        //    }
        //    catch (ArgumentException)
        //    {
        //        return View("NoOrdersFound");
        //    }

        //    return View(model);
        //}

        //[HttpGet]
        //public async Task<IActionResult> SearchOrderByPartner([FromQuery]OrderViewModel model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.Partner))
        //    {
        //        return View();
        //    }

        //    try
        //    {
        //        var partner = await this.partnerService.FindByNameAsync(model.Partner);
        //        model.SearchResults = (await this.orderService.GetOrdersByPartnerAsync(partner))
        //            .Select(this.orderMapper.MapFrom)
        //            .ToList();
        //    }
        //    catch (ArgumentException)
        //    {
        //        return View("NoOrdersFound");
        //    }

        //    return View(model);
        //}


    }
}
