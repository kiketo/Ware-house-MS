using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp.Mappers;
using WHMSWebApp.Models;

namespace WHMSWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IViewModelMapper<Order, OrderViewModel> orderMapper;

        public SearchController(IOrderService orderService, IViewModelMapper<Order, OrderViewModel> orderMapper)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.orderMapper = orderMapper ?? throw new ArgumentNullException(nameof(orderMapper));
        }

        
        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        public IActionResult Partner()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OrderById([FromQuery]SearchOrderByIdViewModel model)
        {
            if (model.GetOrderById == 0)
            {
                return View();
            }

            model.SearchResults = new List<OrderViewModel>
            {
                this.orderMapper.MapFrom(await this.orderService.GetOrderByIdAsync(model.GetOrderById))
            };

            return View(model);
        }
    }
}