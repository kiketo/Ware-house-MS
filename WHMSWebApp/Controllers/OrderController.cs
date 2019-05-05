using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSData.Utills;
using WHMSWebApp.Mappers;
using WHMSWebApp.Models.OrderViewModels;

namespace WHMSWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IPartnerService partnerService;
        private readonly IViewModelMapper<Order, OrderViewModel> orderMapper;
        private readonly IUnitService unitService;
        private readonly IProductWarehouseService productWarehouseService;

        public OrderController(
            IOrderService orderService,
            IProductService productService,
            IPartnerService partnerService,
            IViewModelMapper<Order, OrderViewModel> orderMapper, IUnitService unitService,
            IProductWarehouseService productWarehouseService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.orderMapper = orderMapper ?? throw new ArgumentNullException(nameof(orderMapper));
            this.unitService = unitService ?? throw new ArgumentNullException(nameof(unitService));
            this.productWarehouseService = productWarehouseService ?? throw new ArgumentNullException(nameof(productWarehouseService));
        }

        public IActionResult Search()
        {
            return View();
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
                return View("NoOrdersFound");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchOrderByPartner([FromQuery]OrderViewModel model)
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
                return View("NoOrdersFound");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchOrderByType([FromQuery]OrderViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Type))
            {
                return View();
            }
            OrderType orderType;
            var type = Enum.TryParse(model.Type, true, out orderType);

            try
            {
                model.SearchResults = (await this.orderService.GetOrdersByTypeAsync(orderType, DateTime.MinValue, DateTime.MaxValue))
                    .Select(this.orderMapper.MapFrom)
                    .ToList();
            }
            catch (ArgumentException)
            {

                return View("NoOrdersFound");
            }

            return View(model);
        }




        [HttpGet]
        [Route("[controller]/[action]/id")]
        public async Task<IActionResult> Create(int warehouseId)
        {
            var productsOverZero = await this.productWarehouseService.GetAllProductsInWarehouseWithQuantityOverZeroAsync(warehouseId);
            var productsList = new List<Product>();
            foreach (var pw in productsOverZero)
            {
                productsList.Add(pw.Product);
            }

            OrderViewModel model = new OrderViewModel()
            {
                ProductsInWarehouse = new SelectList(productsList, "Id","Name").OrderBy(x => x.Text)
                
            };
            return View(model);
        }//TODO



        //[HttpPost]
        //public IActionResult Create(OrderViewModel order)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        var newOrder = this.orderService.AddAsync(
        //            order.Type,
        //            this.partnerService.FindByNameAsync(order.Partner),
        //            this.productService.FindByName(order.Products),//TODO change logic, wrong!!!
        //            order.quantity,//TODO
        //            order.Comment
        //            );

        //        return RedirectToAction(nameof(Details), new { id = newOrder.Id });
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public IActionResult Details(int id)
        {
            var model = this.orderService.GetOrderByIdAsync(id);
            return View(model);
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
