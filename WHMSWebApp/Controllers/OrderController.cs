using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WHMS.Services;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;
using WHMSWebApp.Mappers;
using WHMSWebApp.Models;

namespace WHMSWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IViewModelMapper<Order,OrderViewModel> orderMapper;

        public OrderController(IOrderService orderService, IViewModelMapper<Order, OrderViewModel> orderMapper)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.orderMapper = orderMapper ?? throw new ArgumentNullException(nameof(orderMapper));
        }

        [HttpGet]
        public async Task<IActionResult> SearchById([FromQuery]SearchOrderViewModel model)
        {
            if (model.GetOrderById==0)
            {
                return View();
            }

            model.SearchResults =new List<OrderViewModel>
            {
                this.orderMapper.MapFrom(await this.orderService.GetOrderByIdAsync(model.GetOrderById))
            };

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
