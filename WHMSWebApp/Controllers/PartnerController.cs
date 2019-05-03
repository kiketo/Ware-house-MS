using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Models;
using WHMSWebApp.Mappers;
using WHMSWebApp.Models;

namespace WHMSWebApp.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService partnerService;
        private readonly IViewModelMapper<Partner, PartnerViewModel> partnerMapper;

        public PartnerController(IPartnerService partnerService, IViewModelMapper<Partner, PartnerViewModel> partnerMapper)
        {
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
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

    }
}
