using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp2.Mappers;
using WHMSWebApp2.Models;
using WHMS.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Search()
        {
            return View();
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
                return View("NoPartnerFound");
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
                return View("NoPartnerFound");
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
                return View("NoPartnerFound");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PartnerViewModel model = new PartnerViewModel()
            {
                Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name")
                .OrderBy(x => x.Text)
            };
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerViewModel partner)
        {
            partner.Cities = new SelectList(await this.townService.GetAllTownsAsync(), "Id", "Name").OrderBy(x => x.Text);

            if (ModelState.IsValid)
            {
                Town town = await this.townService.GetTownByIdAsync(int.Parse( partner.City));

                if (town == null || town.IsDeleted)
                {
                    town = await this.townService.AddAsync(partner.City);

                }
                Address address = new Address()
                {
                    Town = town,
                    Text = partner.Address
                };
                //TODO async  method GetAddress
                //await this.addressService.GetAddress(town, partner.Address);

                //if (address == null || address.IsDeleted)
                //{
                //    address = await this.addressService.AddAsync(town, partner.Address);

                //}

                var newPartner = await this.partnerService.AddAsync(
                    partner.Name,
                    address,
                    partner.VAT
                    );
                
                return RedirectToAction(nameof(Details), new { id = newPartner.Id });
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            PartnerViewModel viewModel = partnerMapper.MapFrom(await this.partnerService.FindByIdAsync(id));
            return View(viewModel);
        }

        public IActionResult Edit()
        {
            return View();
        }

    }
}
