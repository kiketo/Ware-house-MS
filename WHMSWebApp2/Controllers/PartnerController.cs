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

namespace WHMSWebApp2.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService partnerService;
        private IAddressService addressService;
        private readonly ITownService townService;
        private readonly IViewModelMapper<Partner, PartnerViewModel> partnerMapper;

        public PartnerController(IPartnerService partnerService, IAddressService addressService, ITownService townService, IViewModelMapper<Partner, PartnerViewModel> partnerMapper)
        {
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.partnerMapper = partnerMapper ?? throw new ArgumentNullException(nameof(partnerMapper));
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Create()
        {


            PartnerViewModel model = new PartnerViewModel()
            {
                //Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name")
                //.OrderBy(x => x.Text),
                //Addresses = new SelectList(listAddress, "Id", "Text")
                //.OrderBy(x => x.Text)
                AddressesList = await this.addressService.GetAllAddressesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerViewModel partner)
        {
            var listAddress = new List<string>();
            foreach (var address in await this.addressService.GetAllAddressesAsync())
            {
                listAddress.Add($"{address.Town.Name} ,{address.Text}");
            }
            //partner.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name").OrderBy(x => x.Text);
            partner.Addresses = new SelectList(await this.addressService.GetAllAddressesAsync(), "Id", "Text").OrderBy(x => x.Text);
            if ((await this.partnerService.GetAllPartners()).Any(p=>p.Name == partner.Name))
            {
                ModelState.AddModelError("Name", "Name is already used");
            }
            ModelState.Remove("Id");
            ModelState.Remove("City");

            if (ModelState.IsValid)
            {
                Town town = await this.townService.GetTownByIdAsync(int.Parse(partner.City));

                if (town == null || town.IsDeleted)
                {
                    town = await this.townService.AddAsync(partner.City);
                }
                Address address = await this.addressService.GetAddressAsync(town, int.Parse(partner.Address));

                //if (address == null || address.IsDeleted)
                //{
                //    address = await this.addressService.AddAsync(town, partner.Address);

                //}

                var newPartner = new Partner();// await this.partnerService.AddAsync(
                //    partner.Name,
                //    await this.addressService.GetAddressAsync((await this.addressService.GetTownByIdAsync(int.Parse(partner.City))), int.Parse(partner.Address)),
                //    partner.VAT
                //    );

                return RedirectToAction(nameof(Details), new { id = newPartner.Id });
            }
            else
            {
                return View(partner);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            PartnerViewModel model = partnerMapper.MapFrom(await this.partnerService.FindByIdAsync(id));

            model.CanUserEdit = model.CreatorId == this.User.GetId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");
            model.CanUserDelete = this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin");

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            PartnerViewModel model = this.partnerMapper.MapFrom(await this.partnerService.FindByIdAsync(id));
            model.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name")
                         .OrderBy(x => x.Text);
            return View(model);
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Partner partnerToDelete = await this.partnerService.DeleteAsync(id);
            PartnerViewModel model = this.partnerMapper.MapFrom(partnerToDelete);

            return View(model);
        }
    }
}
