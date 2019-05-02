using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp.Models;

namespace WHMSWebApp.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService partnerService;
        private IAddressService addressService;
        private readonly ITownService townService;

        public PartnerController(IPartnerService partnerService, IAddressService addressService, ITownService townService)
        {
            this.partnerService = partnerService;
            this.addressService = addressService;
            this.townService = townService;
        }
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerViewModel partner)
        {
            if (ModelState.IsValid)
            {
                Town town = this.townService.GetTown(partner.City);

                if (town == null || town.IsDeleted)
                {
                    town = this.townService.Add(partner.City);

                }
                Address address = this.addressService.GetAddress(town, partner.Address);

                if (address == null || address.IsDeleted)
                {
                    address = await this.addressService.AddAsync(town, partner.Address);

                }

                var newPartner = this.partnerService.AddAsync(
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

        public IActionResult Details(int id)
        {
            var model = this.partnerService.FindByIdAsync(id);
            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }

    }
}
