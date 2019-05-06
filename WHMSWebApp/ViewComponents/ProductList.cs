using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;

namespace WHMSWebApp.ViewComponents
{
    public class ProductList :ViewComponent
    {
        private readonly IProductService productService;

        public ProductList(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IViewComponentResult> Invoke()
        {
            var modelProducts = new SelectList (await productService.GetAllProductsAsync(), "Id", "Name").OrderBy(x => x.Text).ToList();
           
            return View("Default", modelProducts);

        }
    }
}
