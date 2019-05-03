using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp.Mappers;
using WHMSWebApp.Models;

namespace WHMSWebApp.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;
        IUnitService unitService;
        ICategoryService categoryService;
        IViewModelMapper<Product,ProductViewModel> productMapper;

        public ProductController(IProductService productService, IUnitService unitService, ICategoryService categoryService, IViewModelMapper<Product, ProductViewModel> productMapper)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.unitService = unitService ?? throw new ArgumentNullException(nameof(unitService));
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
        }

        [HttpGet]
        public IActionResult Create()
        {
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
                    unitService.GetUnit(product.Unit),
                    categoryService.FindByName(product.Category),
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
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.productService.GetProductByIdAsync(id);
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

        public async Task<IActionResult> SearchProductById([FromQuery]ProductViewModel model)
        {
            if (model.Id == 0)
            {
                return View();
            }
            try
            {
                model.SearchResults = new List<ProductViewModel>
            {
                this.productMapper.MapFrom(await this.productService.GetProductByIdAsync(model.Id))
            };
            }
            catch (ArgumentException)
            {
                return View("NoProductFound");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchProductByName([FromQuery]ProductViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return View();
            }

            try
            {
                model.SearchResults = (await this.productService.FindByNameAsync(model.Name))
                    .Select(this.productMapper.MapFrom)
                    .ToList();
            }
            catch (ArgumentException)
            {
                return View("NoOrdersFound");
            }

            return View(model);
        }


    }
}
