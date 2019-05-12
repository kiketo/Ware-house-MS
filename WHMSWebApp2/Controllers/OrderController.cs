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
        private readonly ITownService townService;
        private readonly IAddressService addressService;

        public OrderController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOrderService orderService,
            IProductService productService,
            IPartnerService partnerService,
            IViewModelMapper<Order, OrderViewModel> orderMapper,
            IUnitService unitService, IProductWarehouseService productWarehouseService,
            IWarehouseService warehouseService, IViewModelMapper<Warehouse,
                WarehouseViewModel> warehouseModelMapper,
            IOrderProductWarehouseService orderProductWarehouseService,
            ITownService townService,
            IAddressService addressService
            )
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
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var order = new OrderViewModel()
            {
                ListWarehouses = await this.warehouseService.GetAllWarehousesAsync(),
                Partners = new SelectList(await this.partnerService.GetAllPartners(), "Id", "Name").OrderBy(x => x.Text),
                ListTowns = await this.townService.GetAllTownsAsync(),
                ListAddresses = await this.addressService.GetAllAddressesAsync()
            };

            return View("Create", order);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            model.ListWarehouses = await this.warehouseService.GetAllWarehousesAsync();
            model.Partners = new SelectList(await this.partnerService.GetAllPartners(), "Id", "Name").OrderBy(x => x.Text);
            model.ListTowns = await this.townService.GetAllTownsAsync();
            model.ListAddresses = await this.addressService.GetAllAddressesAsync();
            
            int warehouseId = model.WarehouseId;
            Warehouse newWarehouse;
            Address newAddress;
            Town newTown;
            if (model.WarehouseId == 0)
            {
                
                if (model.AddressId == 0)
                {
                    
                    if (model.TownId ==0 )
                    {
                        
                        if (model.Town != null && model.AddressLine !=null && model.Warehouse != null)
                        {
                            newTown = await this.townService.AddAsync(model.Town);
                            newAddress = await this.addressService.AddAsync(newTown, model.AddressLine);
                            newWarehouse = await this.warehouseService.CreateWarehouseAsync(model.Warehouse, newAddress);

                            ModelState.Remove("AddressId");
                            ModelState.Remove("TownId");
                            ModelState.Remove("WarehouseId");
                        }
                        else
                        {
                            ModelState.AddModelError("Warehouse", "Warehouse is required!");
                        }
                        
                    }
                    else if (model.AddressLine != null)
                    {
                        ModelState.Remove("AddressId");
                        ModelState.Remove("WarehouseId");
                        newAddress = await this.addressService.AddAsync(await this.townService.GetTownByIdAsync(model.TownId), model.AddressLine);
                        newWarehouse = await this.warehouseService.CreateWarehouseAsync(model.Warehouse, newAddress);
                    }
                    else
                    {
                        ModelState.AddModelError("Warehouse", "Warehouse is required!");
                    }
                   
                }
                else if (model.AddressId != 0)
                {
                    ModelState.Remove("TownId");
                    ModelState.Remove("WarehouseId");
                    newWarehouse = await this.warehouseService.CreateWarehouseAsync(model.Warehouse, await this.addressService.GetAddressByIdAsync(model.AddressId));
                }
            }
            else
            {
                
                ModelState.Remove("AddressId");
                ModelState.Remove("TownId");
            }

               
           
            int partnerId;
            int.TryParse(model.Partner, out partnerId);
            if (partnerId == 0 || !(await this.partnerService.GetAllPartners()).Any(o => o.Id == partnerId))
            {
                ModelState.AddModelError("Partner", "Partner is required!");
            }
            OrderType type;
            if (Enum.TryParse(model.Type, out type))
            {
                model.TypeOrder = type;
            }
            else
            {
                ModelState.AddModelError("Type", "Type is required!");
            }


            if (ModelState.IsValid)
            {
                var pwList = await this.productWarehouseService.GetAllProductsInWarehouseAsync(model.WarehouseId);
                var productsQuantityStock = new Dictionary<ProductWarehouse, int>();
                foreach (var pw in pwList)
                {
                    productsQuantityStock.Add(pw, 0);
                }
                ApplicationUser user = await this.userManager.GetUserAsync(User);
                var partner = await this.partnerService.GetByIdAsync(int.Parse(model.Partner));
                var newOrder = await this.orderService.AddAsync(
                    model.TypeOrder,
                    partner,
                    model.WantedQuantityByProduct = productsQuantityStock,
                    user,
                    model.Comment
                    );

                return RedirectToAction(nameof(ChooseProduct), new { id = newOrder.Id });
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        [ActionName(nameof(ChooseProduct))]
        public async Task<IActionResult> ChooseProduct(int id)
        {
            var order = await this.orderService.GetOrderByIdAsync(id);
            var model = orderMapper.MapFrom(order);

            var listProductStock = new Dictionary<Product, int>();
            foreach (var p in model.ProductsQuantitiesOPW.Where(q => q.WantedQuantity == 0))
            {
                listProductStock.Add(
                    await this.productService.GetProductByIdAsync(p.ProductId),
                    await this.productWarehouseService.GetQuantityAsync(p.ProductId, p.WarehouseId));
            }
            model.ProductsQuantity = listProductStock;
            model.listProductsWithQuantities = new List<OrderProductViewModel>();
            foreach (var item in model.ProductsQuantitiesOPW.Where(w => w.WantedQuantity > 0))
            {
                model.listProductsWithQuantities.Add(new OrderProductViewModel()
                {
                    Product = await this.productService.GetProductByIdAsync(item.ProductId),
                    InStock = await this.productWarehouseService.GetQuantityAsync(item.ProductId, item.WarehouseId),
                    WantedQuantity = item.WantedQuantity
                });
            }


            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(ChooseProduct))]
        public async Task<IActionResult> ChooseProduct(OrderViewModel model, int id)
        {
            var order = await this.orderService.GetOrderByIdAsync(id);
            model.TypeOrder = order.Type;
            model.WarehouseId = order.OrderProductsWarehouses.Select(w => w.WarehouseId).First();
            var listProductStock = new List<OrderProductViewModel>();
            foreach (var p in order.OrderProductsWarehouses.Where(q => q.WantedQuantity == 0))
            {
                listProductStock.Add(new OrderProductViewModel()
                {
                    ProductId = p.ProductId,
                    Product = await this.productService.GetProductByIdAsync(p.ProductId),
                    InStock = await this.productWarehouseService.GetQuantityAsync(p.ProductId, p.WarehouseId)
                });
            }
            model.List2ProductsWithQuantities = listProductStock;
            model.listProductsWithQuantities = new List<OrderProductViewModel>();
            foreach (var item in order.OrderProductsWarehouses.Where(w => w.WantedQuantity > 0))
            {
                model.listProductsWithQuantities.Add(new OrderProductViewModel()
                {
                    Product = await this.productService.GetProductByIdAsync(item.ProductId),
                    InStock = await this.productWarehouseService.GetQuantityAsync(item.ProductId, item.WarehouseId),
                    WantedQuantity = item.WantedQuantity
                });
            }

            var wantedquantity = model.WantedQuantity;
            var selectedProduct = model.ProductId;
            ModelState.Remove("Type");
            if (model.TypeOrder == OrderType.Buy
                && model.WantedQuantity > await this.productWarehouseService.GetQuantityAsync(model.ProductId, model.WarehouseId))
            {
                ModelState.AddModelError("WantedQuantity", "There are not enough items in stock");
            }
            if (model.WantedQuantity < 0)
            {
                ModelState.AddModelError("WantedQuantity", "Quantity cannot be negative! ");
            }
            if (ModelState.IsValid)
            {
                var opw =
                    await this.orderProductWarehouseService.GetOPW(
                        selectedProduct,
                        model.WarehouseId,
                        model.Id);
                opw.WantedQuantity = wantedquantity;
                await this.orderProductWarehouseService.UpdateWantedQuantity(opw);
                if (model.TypeOrder == OrderType.Buy)
                {
                    order.TotalValue += wantedquantity * (await this.productService.GetProductByIdAsync(opw.ProductId)).BuyPrice;
                    await this.orderService.UpdateAsync(order);
                    await this.productWarehouseService.SubstractQuantityAsync(opw.ProductId, opw.WarehouseId, opw.WantedQuantity);
                }
                else
                {
                    order.TotalValue += wantedquantity * (await this.productService.GetProductByIdAsync(opw.ProductId)).SellPrice;
                    await this.orderService.UpdateAsync(order);
                    await this.productWarehouseService.AddQuantityAsync(opw.ProductId, opw.WarehouseId, opw.WantedQuantity);
                }

                if (model.RedirectDetails)
                {

                    return RedirectToAction("Details", new { id = model.Id });
                }
                return RedirectToAction("ChooseProduct", id);
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

            var listOrdered = new List<OrderProductViewModel>();
            foreach (var opw in model.ProductsQuantitiesOPW.Where(q => q.WantedQuantity > 0))
            {
                listOrdered.Add(new OrderProductViewModel()
                {
                    Product = await this.productService.GetProductByIdAsync(opw.ProductId),
                    WantedQuantity = opw.WantedQuantity
                });
            }
            model.SelectedProductsWithQuantities = listOrdered;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderViewModel model)
        {

            var pwList = await this.productWarehouseService.GetAllProductsInWarehouseAsync(model.WarehouseId);
            var listProducts = new List<OrderProductViewModel>();
            foreach (var pw in pwList)
            {
                listProducts.Add(new OrderProductViewModel()
                {
                    InStock = pw.Quantity + model.WantedQuantityByProduct[pw],
                    Product = await this.productService.GetProductByIdAsync(pw.ProductId),
                    WantedQuantity = model.WantedQuantityByProduct[pw]
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
                updatedOrder.Partner = await this.partnerService.GetByNameAsync(model.Partner);
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
            var listOrdered = new List<OrderProductViewModel>();
            foreach (var opw in model.ProductsQuantitiesOPW.Where(q => q.WantedQuantity > 0))
            {
                listOrdered.Add(new OrderProductViewModel()
                {
                    Product = await this.productService.GetProductByIdAsync(opw.ProductId),
                    WantedQuantity = opw.WantedQuantity
                });
            }
            model.SelectedProductsWithQuantities = listOrdered;
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
                var partner = await this.partnerService.GetByNameAsync(model.Partner);
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
