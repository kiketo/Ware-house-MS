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

namespace WHMSWebApp2.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService partnerService;
        private IAddressService addressService;
        private readonly ITownService townService;
        private readonly IViewModelMapper<Partner, PartnerViewModel> partnerMapper;
        private readonly IOrderService orderService;

        public PartnerController(IPartnerService partnerService, 
            IAddressService addressService, 
            ITownService townService, 
            IViewModelMapper<Partner, PartnerViewModel> partnerMapper,
            IOrderService orderService
            )
        {
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.partnerMapper = partnerMapper ?? throw new ArgumentNullException(nameof(partnerMapper));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet]
        public async Task<IActionResult> SearchPartnerById([FromQuery]PartnerViewModel model)
        {
            if (model.Id == 0)
            {
                return View();
            }
            try
            {
                model.SearchResult = this.partnerMapper.MapFrom(await this.partnerService.FindByIdAsync(model.Id));
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchPartnerByName([FromQuery]PartnerViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return View();
            }

            try
            {
                model.SearchResult = this.partnerMapper.MapFrom(await this.partnerService.FindByNameAsync(model.Name));
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchPartnerByVAT([FromQuery]PartnerViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.VAT))
            {
                return View();
            }

            try
            {
                model.SearchResult = this.partnerMapper.MapFrom(await this.partnerService.FindByVATAsync(model.VAT));
            }
            catch (ArgumentException)
            {
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerViewModel partner)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Address");
            ModelState.Remove("City");
            if ((await this.partnerService.GetAllPartners()).Any(p => p.Name == partner.Name))
            {
                ModelState.AddModelError("Name", "Name is already used");
            }
            if (partner.AddressId == 0 && partner.City == null || 
                partner.AddressId == 0 && partner.City == "Please choose a city")
            {
                ModelState.AddModelError("City", "City is required");
            }
            if (partner.AddressId == 0 && partner.Address == null)
            {
                ModelState.AddModelError("Address", "Address is required");
            }
            if (partner.AddressId == 0)
            {
                ModelState.Remove("AddressId");
            }
            partner.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name").OrderBy(x => x.Text);
            partner.AddressesList = await this.addressService.GetAllAddressesAsync();

            if (ModelState.IsValid)
            {
                Town town;
                Address address;
                if (partner.AddressId == 0)
                {
                    town = await this.townService.GetTownByIdAsync(int.Parse(partner.City));

                    if (town == null || town.IsDeleted)
                    {
                        town = await this.townService.AddAsync(partner.City);
                    }
                    address = await this.addressService.AddAsync(town, partner.Address);
                }
                else
                {
                    address = await this.addressService.GetAddressByIdAsync(partner.AddressId);
                }

               

                var newPartner = await this.partnerService.AddAsync(
                partner.Name,
                    address,
                    partner.VAT
                    );

                return RedirectToAction(nameof(Details), new { id = newPartner.Id });
            }
            else
            {
                return View(partner);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            PartnerViewModel viewModel = partnerMapper.MapFrom(await this.partnerService.FindByIdAsync(id));
           // viewModel.Orders = (await this.orderService.GetOrdersByPartnerAsync((await this.partnerService.FindByIdAsync(id)))).OrderBy(x=>x.ModifiedOn).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PartnerViewModel model = this.partnerMapper.MapFrom(await this.partnerService.FindByIdAsync(id));
            model.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name")
                         .OrderBy(x => x.Text);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartnerViewModel model)
        {
            model.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name").OrderBy(x => x.Text);

            if (ModelState.IsValid)
            {
                Town town = await this.townService.GetTownByIdAsync(int.Parse(model.City));

                if (town == null || town.IsDeleted)
                {
                    town = await this.townService.AddAsync(model.City);
                }
                Address address = new Address()
                {
                    Town = town,
                    Text = model.Address
                };
                //TODO async  method GetAddress
                //await this.addressService.GetAddress(town, partner.Address);

                //if (address == null || address.IsDeleted)
                //{
                //    address = await this.addressService.AddAsync(town, partner.Address);

                //}
                Partner updatedPartner = await this.partnerService.FindByIdAsync(model.Id);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Partner partnerToDelete = await this.partnerService.DeleteAsync(id);
            PartnerViewModel model = this.partnerMapper.MapFrom(partnerToDelete);

            return View(model);
        }
    }
}
