using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSData.Utills;
using WHMSWebApp2.Extensions;
using WHMSWebApp2.Mappers;
using WHMSWebApp2.Models;

namespace WHMSWebApp2.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IPartnerService partnerService;
        private readonly IViewModelMapper<Order, OrderViewModel> orderMapper;
        private readonly IUnitService unitService;
        private readonly IProductWarehouseService productWarehouseService;
        private readonly IWarehouseService warehouseService;
        private readonly IViewModelMapper<Warehouse, WarehouseViewModel> warehouseModelMapper;
        private readonly IOrderProductWarehouseService orderProductWarehouseService;

        public OrderController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOrderService orderService, IProductService productService, IPartnerService partnerService, IViewModelMapper<Order, OrderViewModel> orderMapper, IUnitService unitService, IProductWarehouseService productWarehouseService, IWarehouseService warehouseService, IViewModelMapper<Warehouse, WarehouseViewModel> warehouseModelMapper, IOrderProductWarehouseService orderProductWarehouseService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.orderMapper = orderMapper ?? throw new ArgumentNullException(nameof(orderMapper));
            this.unitService = unitService ?? throw new ArgumentNullException(nameof(unitService));
            this.productWarehouseService = productWarehouseService ?? throw new ArgumentNullException(nameof(productWarehouseService));
            this.warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
            this.warehouseModelMapper = warehouseModelMapper ?? throw new ArgumentNullException(nameof(warehouseModelMapper));
            this.orderProductWarehouseService = orderProductWarehouseService ?? throw new ArgumentNullException(nameof(orderProductWarehouseService));
        }

        [HttpGet]
        [Authorize]
        [ActionName(nameof(ChooseWarehouse))]
        public async Task<IActionResult> ChooseWarehouse() //choose a warehouse
        {
            var order = new OrderViewModel()
            {
                Warehouses = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "Id", "Name").OrderBy(x => x.Text)
            };

            return View("ChooseWarehouse", order);

        }

        [HttpPost]
        [Authorize]
        [ActionName(nameof(ChooseWarehouse))]
        public async Task<IActionResult> ChooseWarehouse(OrderViewModel model) //choose a warehouse
        {

            if (model.Warehouse == null)
            {
                ModelState.AddModelError("Warehouse", "Choose Warehouse");
                model.Warehouses = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "Id", "Name").OrderBy(x => x.Text);
                return View("ChooseWarehouse", model);
            }

            return RedirectToAction(nameof(Create), new { id = int.Parse(model.Warehouse) });
        }

        [HttpGet]
        [Authorize]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create(int warehouseId)
        {
            var OrderProductModels = new List<OrderProductViewModel>();
            var pwList = await this.productWarehouseService.GetAllProductsInWarehouseWithQuantityOverZeroAsync(warehouseId);
            foreach (var pw in pwList)
            {
                OrderProductModels.Add(new OrderProductViewModel()
                {
                    inStock = pw.Quantity,
                    product = await this.productService.GetProductByIdAsync(pw.ProductId),
                    wantedQuantity = 0

                });
            }
            var model = new OrderViewModel()
            {
                listProductsWithQuantities = OrderProductModels,
                Partners = new SelectList(await this.partnerService.GetAllPartners(), "Id", "Name").OrderBy(x => x.Text)
                
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create(OrderViewModel model, int id)
        {
            var pwList = await this.productWarehouseService.GetAllProductsInWarehouseWithQuantityOverZeroAsync(id);
            var listProducts = new List<OrderProductViewModel>();
            foreach (var pw in pwList)
            {
                listProducts.Add(new OrderProductViewModel()
                {
                    inStock = pw.Quantity,
                    product = await this.productService.GetProductByIdAsync(pw.ProductId),
                    wantedQuantity = 0

                });
            }
           model.SelectedProductsWithQuantities = listProducts;
            model.Partners = new SelectList(await this.partnerService.GetAllPartners(), "Id", "Name").OrderBy(x => x.Text);
            if (!(await this.partnerService.GetAllPartners()).Any(o => o.Id == int.Parse(model.Partner)))
            {
                ModelState.AddModelError("Partner", "Partner is required!");
            }
            if (model.SelectedProductsWithQuantities.Count == 0)
            {
                ModelState.AddModelError("SelectedProductsWithQuantities", "At least one product is required!");
            }
            
            if (ModelState.IsValid)
            {
                ApplicationUser user = await this.userManager.GetUserAsync(User);

                var newOrder = await this.orderService.AddAsync(
                    model.TypeOrder,
                    await this.partnerService.FindByIdAsync(int.Parse(model.Partner)),
                    model.WantedQuantityByProduct,user,model.Comment
                    );

                return RedirectToAction(nameof(Details), new { id = newOrder.Id });
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            OrderViewModel model = this.orderMapper.MapFrom(await this.orderService.GetOrderByIdAsync(id));
            
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderViewModel model)
        {

            var pwList = await this.productWarehouseService.GetAllProductsInWarehouseWithQuantityOverZeroAsync(model.WarehouseId);
            var listProducts = new List<OrderProductViewModel>();
            foreach (var pw in pwList)
            {
                listProducts.Add(new OrderProductViewModel()
                {
                    inStock = pw.Quantity+model.WantedQuantityByProduct[pw],
                    product = await this.productService.GetProductByIdAsync(pw.ProductId),
                    wantedQuantity = model.WantedQuantityByProduct[pw]
                });
            }
            model.SelectedProductsWithQuantities = listProducts;
            model.Partners = new SelectList(await this.partnerService.GetAllPartners(), "Id", "Name").OrderBy(x => x.Text);
            if (!(await this.partnerService.GetAllPartners()).Any(o => o.Id == int.Parse(model.Partner)))
            {
                ModelState.AddModelError("Partner", "Partner is required!");
            }
            if (model.SelectedProductsWithQuantities.Count == 0)
            {
                ModelState.AddModelError("SelectedProductsWithQuantities", "At least one product is required!");
            }

            if (ModelState.IsValid)
            {
                var updatedOrder = await this.orderService.GetOrderByIdAsync(model.Id);

                updatedOrder.Comment = model.Comment;
                updatedOrder.ModifiedOn = DateTime.Now;
                //updatedOrder.OrderProductsWarehouses=model.???
                updatedOrder.Partner = await this.partnerService.FindByNameAsync(model.Partner);
                updatedOrder.TotalValue = model.TotalValue;
                updatedOrder.Type = model.TypeOrder;

                await this.orderService.UpdateAsync(updatedOrder);

                return RedirectToAction(nameof(Details), new { id = updatedOrder.Id });
            }
            else
            {
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var order = await this.orderService.GetOrderByIdAsync(id);
            var model = orderMapper.MapFrom(order);

            var orderProductsIdNQuantities = await this.orderProductWarehouseService.GetProductsByOrderIdWhereWantedQuantityIsOverZeroAsync(id);
            foreach (var opw in orderProductsIdNQuantities)
            {
                model.ProductsQuantity.Add((await this.productService.GetProductByIdAsync(opw.ProductId)), opw.WantedQuantity);
            }

            model.CanUserEdit = model.CreatorId == this.User.GetId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            model.CanUserDelete = this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Order orderToDelete = await this.orderService.DeleteOrderAsync(id);
            OrderViewModel model = this.orderMapper.MapFrom(orderToDelete);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchOrderById([FromQuery]OrderViewModel model)
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
        public async Task<IActionResult> SearchOrdersByPartner([FromQuery]OrderViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Partner))
            {
                return View();
            }

            try
            {
                var partner = await this.partnerService.FindByNameAsync(model.Partner);
                model.SearchResults = (await this.orderService.GetOrdersByPartnerAsync(partner))
                    .Select(this.orderMapper.MapFrom)
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
        public async Task<IActionResult> SearchOrdersByType([FromQuery]OrderViewModel model)
        {
            if (model.Type != "Sell" && model.Type != "Buy")
            {
                return View(model);
            }

            DateTime fromDate = model.FromDate ?? DateTime.MinValue;
            DateTime toDate = model.ToDate ?? DateTime.MaxValue;

            OrderType orderType;
            var type = Enum.TryParse(model.Type, true, out orderType);

            try
            {
                model.SearchResults = (await this.orderService.GetOrdersByTypeAsync(orderType, fromDate, toDate))
                    .Select(this.orderMapper.MapFrom)
                    .ToList();

                return View(model);
            }
            catch (ArgumentException)
            {
                return View(model);
            }
        }
    }
}
