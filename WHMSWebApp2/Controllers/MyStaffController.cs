using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp2.Extensions;
using WHMSWebApp2.Mappers;
using WHMSWebApp2.Models;

namespace WHMSWebApp2.Controllers
{
    public class MyStaffController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IPartnerService partnerService;
        private IViewModelMapper<Product, ProductViewModel> productMapper;
        private readonly IViewModelMapper<Order, OrderViewModel> orderMapper;
        private readonly IViewModelMapper<Partner, PartnerViewModel> partnerMapper;

        public MyStaffController(UserManager<ApplicationUser> userManager, IOrderService orderService, IProductService productService, IPartnerService partnerService, IViewModelMapper<Product, ProductViewModel> productMapper, IViewModelMapper<Order, OrderViewModel> orderMapper, IViewModelMapper<Partner, PartnerViewModel> partnerMapper)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
            this.orderMapper = orderMapper ?? throw new ArgumentNullException(nameof(orderMapper));
            this.partnerMapper = partnerMapper ?? throw new ArgumentNullException(nameof(partnerMapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyProducts()
        {
            var userId = User.GetId();

            var model = new ProductViewModel
            {
                SearchResults = (await this.productService.GetProductsByCreatorId(userId))
                    .Select(this.productMapper.MapFrom)
                    .ToList()
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyOrders([FromQuery]OrderViewModel model)
        {
            if (model.Id == 0)
            {
                return View();
            }
            try
            {
                model.SearchResults = new List<OrderViewModel>
            {
                this.orderMapper.MapFrom(await this.orderService.GetOrderByIdAsync(model.Id))
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
        public async Task<IActionResult> MyPartners()
        {
            var userId = User.GetId();

            var model = new PartnerViewModel
            {
                SearchResults = (await this.partnerService.GetPartnersByCreatorId(userId))
                    .Select(this.partnerMapper.MapFrom)
                    .ToList()
            };

            return View(model);
        }
    }
}