using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSData.Utills;
using WHMSWebApp2.Mappers;
using WHMSWebApp2.Models;
using WHMSWebApp2.Models.OrderViewModels;

namespace WHMSWebApp2.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IPartnerService partnerService;
        private readonly IViewModelMapper<Order, OrderViewModel> orderMapper;
        private readonly IUnitService unitService;
        private readonly IProductWarehouseService productWarehouseService;
        private readonly IWarehouseService warehouseService;
        private readonly IViewModelMapper<Warehouse, WarehouseViewModel> warehouseModelMapper;
        private readonly IOrderProductWarehouseService orderProductWarehouseService;

        public OrderController(
            IOrderService orderService,
            IProductService productService,
            IPartnerService partnerService,
            IViewModelMapper<Order, OrderViewModel> orderMapper, IUnitService unitService,
            IProductWarehouseService productWarehouseService,
            IWarehouseService warehouseService,
            IViewModelMapper<Warehouse, WarehouseViewModel> warehouseModelMapper,
            IOrderProductWarehouseService orderProductWarehouseService)
        {
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

        [HttpGet]
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
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create(int id)
        {
            var listProducts = new List<OrderProductViewModel>();
            var pw = await this.productWarehouseService.GetAllProductsInWarehouseWithQuantityOverZeroAsync(id);
            foreach (var product in pw)
            {
                listProducts.Add(new OrderProductViewModel()
                {
                    inStock = product.Quantity,
                    product = await this.productService.GetProductByIdAsync(product.ProductId),
                    wantedQuantity = 0

                });
            }
            var order = new OrderViewModel()
            {
                listProductsWithQuantities = listProducts,
                Partners = new SelectList(await this.partnerService.GetAllPartners(), "Id", "Name").OrderBy(x => x.Text)
                
            };

            return View("Create", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create(OrderViewModel order, int id)
        {
            var pw = await this.productWarehouseService.GetAllProductsInWarehouseWithQuantityOverZeroAsync(id);
            var listProducts = new List<OrderProductViewModel>();
            foreach (var product in pw)
            {
                listProducts.Add(new OrderProductViewModel()
                {
                    inStock = product.Quantity,
                    product = await this.productService.GetProductByIdAsync(product.ProductId),
                    wantedQuantity = 0

                });
            }
           order.SelectedProductsWithQuantities = listProducts;
            order.Partners = new SelectList(await this.partnerService.GetAllPartners(), "Id", "Name").OrderBy(x => x.Text);
            if (!(await this.partnerService.GetAllPartners()).Any(o => o.Id == int.Parse(order.Partner)))
            {
                ModelState.AddModelError("Partner", "Partner is required!");
            }
            if (order.SelectedProductsWithQuantities.Count == 0)
            {
                ModelState.AddModelError("SelectedProductsWithQuantities", "At least one product is required!");
            }
            

            if (ModelState.IsValid)
            {
                var newOrder = await this.orderService.AddAsync(
                    order.TypeOrder,
                    await this.partnerService.FindByIdAsync(int.Parse(order.Partner)),
                    order.WantedQuantityByProduct
                    );

                return RedirectToAction(nameof(Details), new { id = newOrder.Id });
            }
            else
            {
                return View(order);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.orderService.GetOrderByIdAsync(id);
            var viewModel = orderMapper.MapFrom(model);

            var orderProductsIdNQuantities = await this.orderProductWarehouseService.GetProductsByOrderIdWhereWantedQuantityIsOverZeroAsync(id);
            foreach (var opw in orderProductsIdNQuantities)
            {
                viewModel.ProductsQuantity.Add((await this.productService.GetProductByIdAsync(opw.ProductId)), opw.WantedQuantity);
            }

            return View(viewModel);
        }




        //// GET: Orders
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = context.Orders.Include(o => o.Creator).Include(o => o.Partner);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //// GET: Orders/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await context.Orders
        //        .Include(o => o.Creator)
        //        .Include(o => o.Partner)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["CreatorId"] = new SelectList(context.Users, "Id", "Id");
        //    ViewData["PartnerId"] = new SelectList(context.Partners, "Id", "Name");
        //    return View();
        //}

        //// POST: Orders/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Type,PartnerId,Comment,TotalValue,CreatorId,Id,CreatedOn,ModifiedOn,IsDeleted")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Add(order);
        //        await context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CreatorId"] = new SelectList(context.Users, "Id", "Id", order.CreatorId);
        //    ViewData["PartnerId"] = new SelectList(context.Partners, "Id", "Name", order.PartnerId);
        //    return View(order);
        //}

        //// GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CreatorId"] = new SelectList(context.Users, "Id", "Id", order.CreatorId);
        //    ViewData["PartnerId"] = new SelectList(context.Partners, "Id", "Name", order.PartnerId);
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Type,PartnerId,Comment,TotalValue,CreatorId,Id,CreatedOn,ModifiedOn,IsDeleted")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            context.Update(order);
        //            await context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CreatorId"] = new SelectList(context.Users, "Id", "Id", order.CreatorId);
        //    ViewData["PartnerId"] = new SelectList(context.Partners, "Id", "Name", order.PartnerId);
        //    return View(order);
        //}

        //// GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await context.Orders
        //        .Include(o => o.Creator)
        //        .Include(o => o.Partner)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await context.Orders.FindAsync(id);
        //    context.Orders.Remove(order);
        //    await context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(int id)
        //{
        //    return context.Orders.Any(e => e.Id == id);
        //}
    }
}
