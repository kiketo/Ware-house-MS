using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp2.Mappers;
using WHMSWebApp2.Models;
using WHMSWebApp2.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace WHMSWebApp2.Controllers
{
    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private IProductService productService;
        private IUnitService unitService;
        private ICategoryService categoryService;
        private IProductWarehouseService productWarehouseService;
        private IViewModelMapper<Product, ProductViewModel> productMapper;

        public ProductController(UserManager<ApplicationUser> userManager, IProductService productService, IUnitService unitService, ICategoryService categoryService, IProductWarehouseService productWarehouseService, IViewModelMapper<Product, ProductViewModel> productMapper)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.unitService = unitService ?? throw new ArgumentNullException(nameof(unitService));
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            this.productWarehouseService = productWarehouseService ?? throw new ArgumentNullException(nameof(productWarehouseService));
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ProductViewModel product = new ProductViewModel()
            {
                ListCategories = (await this.categoryService.GetAllCategoriesAsync()).ToList(),
                LisUnits = ((await this.unitService.GetAllUnitsAsync()).ToList())
                //Categories = new SelectList(await this.categoryService.GetAllCategoriesAsync(), "Id", "Name")
                //.OrderBy(x => x.Text),
                //Units = new SelectList(await this.unitService.GetAllUnitsAsync(), "Id", "UnitName")
                //.OrderBy(x => x.Text)
            };
            return View(product);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            //var allProducts = await this.productService.GetAllProductsAsync();

            model.ListCategories = (await this.categoryService.GetAllCategoriesAsync()).ToList();
            model.LisUnits = ((await this.unitService.GetAllUnitsAsync()).ToList());
            //if (allProducts.Any(p => p.Name == model.Name))
            //{
            //    ModelState.AddModelError("Name", "The name must be unique.");
            //}
            ModelState.Remove("Unit");
            ModelState.Remove("UnitId");
            ModelState.Remove("CategoryId");
            if (model.UnitId == 0 && model.NewUnit == null)
            {
                ModelState.AddModelError("UnitId", "Unit is Required");
            }
            if (ModelState.IsValid)
            {
                Unit unit= await this.unitService.GetUnitByIDAsync(model.UnitId);
                Category category = await this.categoryService.GetCategoryByNameAsync(model.Category);
                ApplicationUser user = await this.userManager.GetUserAsync(User);
                if (model.UnitId == null || unit==null)
                {
                    unit = await this.unitService.CreateUnitAsync(model.NewUnit);
                }
                if (model.CategoryId == 0 || category==null)
                {
                    if (model.NewCategory != null)
                    {
                        category = await this.categoryService.CreateCategoryAsync(model.NewCategory);
                    }
                }
                
                var newProduct = await this.productService.CreateProductAsync(
                    model.Name,
                    unit,
                    category,
                    model.BuyPrice,
                    model.MarginInPercent,
                    model.Description,
                    user
                    );
                var viewModel = productMapper.MapFrom(newProduct);

                return RedirectToAction(nameof(Details), new { id = viewModel.Id });
            }
            else
            {
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            ProductViewModel model = this.productMapper.MapFrom(await this.productService.GetProductByIdAsync(id));

            model.CanUserEdit = model.CreatorId == this.User.GetId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            model.CanUserDelete = this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Product productToDelete = await this.productService.DeleteProductAsync(id);
            ProductViewModel model = this.productMapper.MapFrom(productToDelete);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            ProductViewModel model = this.productMapper.MapFrom(await this.productService.GetProductByIdAsync(id));
            model.Categories = new SelectList(await this.categoryService.GetAllCategoriesAsync(), "Id", "Name")
                                .OrderBy(x => x.Text);
            model.Units = new SelectList(await this.unitService.GetAllUnitsAsync(), "Id", "UnitName")
                               .OrderBy(x => x.Text);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            //var allProducts = await this.productService.GetAllProductsAsync();

            //if (allProducts.Any(p => p.Name == model.Name))
            //{
            //    ModelState.AddModelError("Name", "The name must be unique.");
            //}

            model.Categories = new SelectList(await this.categoryService.GetAllCategoriesAsync(), "Id", "Name")
                                .OrderBy(x => x.Text);
            model.Units = new SelectList(await this.unitService.GetAllUnitsAsync(), "Id", "UnitName")
                               .OrderBy(x => x.Text);

            if (ModelState.IsValid)
            {
                Product updatedProduct = await this.productService.GetProductByIdAsync(model.Id);
                updatedProduct.BuyPrice = model.BuyPrice;
                updatedProduct.Category = await this.categoryService.GetCategoryByIdAsync(int.Parse(model.Category));
                updatedProduct.Description = model.Description;
                updatedProduct.MarginInPercent = model.MarginInPercent;
                updatedProduct.ModifiedOn = DateTime.Now;
                updatedProduct.Name = model.Name;
                updatedProduct.SellPrice = model.SellPrice;
                updatedProduct.Unit = await this.unitService.GetUnitByIDAsync(int.Parse(model.Unit));

                await this.productService.UpdateAsync(updatedProduct);

                return RedirectToAction(nameof(Details), new { id = updatedProduct.Id });
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
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
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchProductsByName([FromQuery]ProductViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return View();
            }

            try
            {
                model.SearchResults = (await this.productService.GetProductsByNameAsync(model.Name))
                    .Select(this.productMapper.MapFrom)
                    .ToList();
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchProductsByCategory([FromQuery]ProductViewModel model)
        {
            ViewData["Category"] = new SelectList(await this.categoryService.GetAllCategoriesAsync(), "Name", "Name").OrderBy(x => x.Text);
            if (string.IsNullOrWhiteSpace(model.Category))
            {
                return View();
            }

            try
            {
                Category cat = await this.categoryService.GetCategoryByNameAsync(model.Category);
                model.SearchResults = (await this.productService.GetProductsByCategoryAsync(cat))
                    .Select(this.productMapper.MapFrom)
                    .ToList();
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }
    }
}
