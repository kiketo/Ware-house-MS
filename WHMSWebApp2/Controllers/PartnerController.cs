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

namespace WHMSWebApp2.Controllers
{
    public class PartnerController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPartnerService partnerService;
        private IAddressService addressService;
        private readonly ITownService townService;
        private readonly IViewModelMapper<Partner, PartnerViewModel> partnerMapper;
        private readonly IOrderService orderService;

        public PartnerController(UserManager<ApplicationUser> userManager, IPartnerService partnerService, IAddressService addressService, ITownService townService, IViewModelMapper<Partner, PartnerViewModel> partnerMapper, IOrderService orderService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.partnerMapper = partnerMapper ?? throw new ArgumentNullException(nameof(partnerMapper));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            PartnerViewModel model = new PartnerViewModel()
            {
                AddressesList = await this.addressService.GetAllAddressesAsync(),
                Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name").OrderBy(x => x.Text)
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerViewModel model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Address");
            ModelState.Remove("City");
            if ((await this.partnerService.GetAllPartners()).Any(p => p.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Name is already used");
            }
            if (model.AddressId == 0 && model.City == null || 
                model.AddressId == 0 && model.City == "Please choose a city")
            {
                ModelState.AddModelError("City", "City is required");
            }
            if (model.AddressId == 0 && model.Address == null)
            {
                ModelState.AddModelError("Address", "Address is required");
            }
            if (model.AddressId == 0)
            {
                ModelState.Remove("AddressId");
            }
            model.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name").OrderBy(x => x.Text);
            model.AddressesList = await this.addressService.GetAllAddressesAsync();

            if (ModelState.IsValid)
            {
                Town town;
                Address address;
                if (model.AddressId == 0)
                {
                    town = await this.townService.GetTownAsync(model.City);

                    if (town == null || town.IsDeleted)
                    {
                        town = await this.townService.AddAsync(model.City);
                    }

                    address = await this.addressService.AddAsync(town, model.Address);
                }
                else
                {
                    address = await this.addressService.GetAddressByIdAsync(model.AddressId);
                }

                ApplicationUser user = await this.userManager.GetUserAsync(User);
                var newPartner = await this.partnerService
                    .AddAsync(model.Name, user,address, model.VAT);

                return RedirectToAction(nameof(Details), new { id = newPartner.Id });
            }
            else
            {
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            PartnerViewModel model = partnerMapper.MapFrom(await this.partnerService.GetByIdAsync(id));
            model.CanUserEdit = model.CreatorId == this.User.GetId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            model.CanUserDelete = this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
           // viewModel.Orders = (await this.orderService.GetOrdersByPartnerAsync((await this.partnerService.FindByIdAsync(id)))).OrderBy(x=>x.ModifiedOn).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            PartnerViewModel model = this.partnerMapper.MapFrom(await this.partnerService.GetByIdAsync(id));
            model.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name")
                         .OrderBy(x => x.Text);
            model.AddressesList = await this.addressService.GetAllAddressesAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartnerViewModel model)
        {

            ModelState.Remove("Id");
            ModelState.Remove("Address");
            ModelState.Remove("City");
            if ((await this.partnerService.GetAllPartners()).Any(p => p.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Name is already used");
            }
            if (model.AddressId == 0 && model.City == null ||
                model.AddressId == 0 && model.City == "Please choose a city")
            {
                ModelState.AddModelError("City", "City is required");
            }
            if (model.AddressId == 0 && model.Address == null)
            {
                ModelState.AddModelError("Address", "Address is required");
            }
            if (model.AddressId == 0)
            {
                ModelState.Remove("AddressId");
            }
            model.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name").OrderBy(x => x.Text);
            model.AddressesList = await this.addressService.GetAllAddressesAsync();

            if (ModelState.IsValid)
            {
                Town town;
                Address address;
                if (model.AddressId == 0)
                {
                    town = await this.townService.GetTownAsync(model.City);

                    if (town == null || town.IsDeleted)
                    {
                        town = await this.townService.AddAsync(model.City);
                    }

                    address = await this.addressService.AddAsync(town, model.Address);
                }
                else
                {
                    address = await this.addressService.GetAddressByIdAsync(model.AddressId);
                }

                var updatedPartner = await this.partnerService.GetByIdAsync(model.Id);
                updatedPartner.Address = address;
                updatedPartner.ModifiedOn = DateTime.Now;
                updatedPartner.Name = model.Name;
                updatedPartner.VAT = model.VAT;

                await this.partnerService.UpdateAsync(updatedPartner);

                return RedirectToAction(nameof(Details), new { id = updatedPartner.Id });
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Partner partnerToDelete = await this.partnerService.DeleteAsync(id);
            PartnerViewModel model = this.partnerMapper.MapFrom(partnerToDelete);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchPartnerById([FromQuery]PartnerViewModel model)
        {
            model.CanUserEdit = model.CreatorId == this.User.GetId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            model.CanUserDelete = this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            if (model.Id == 0)
            {
                return View();
            }
            try
            {
                model.SearchResult = this.partnerMapper.MapFrom(await this.partnerService.GetByIdAsync(model.Id));
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchPartnerByName([FromQuery]PartnerViewModel model)
        {
            model.CanUserEdit = model.CreatorId == this.User.GetId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            model.CanUserDelete = this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return View();
            }

            try
            {
                model.SearchResult = this.partnerMapper.MapFrom(await this.partnerService.GetByNameAsync(model.Name));
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchPartnerByVAT([FromQuery]PartnerViewModel model)
        {
            model.CanUserEdit = model.CreatorId == this.User.GetId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            model.CanUserDelete = this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            if (string.IsNullOrWhiteSpace(model.VAT))
            {
                return View();
            }

            try
            {
                model.SearchResult = this.partnerMapper.MapFrom(await this.partnerService.GetByVATAsync(model.VAT));
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }
    }
}
