using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly ApplicationDbContext context;

        public PartnerService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Partner> AddAsync(string partnerName, Address address = null, string vat = null)
        {
            Partner newPartner = await this.context.Partners.FirstOrDefaultAsync(t => t.Name == partnerName);

            if (newPartner != null)
            {
                if (newPartner.IsDeleted)
                {
                    newPartner.Address = address;
                    newPartner.VAT = vat;
                    newPartner.CreatedOn = DateTime.Now;
                    newPartner.ModifiedOn = DateTime.Now;
                    newPartner.PastOrders = new List<Order>();
                    newPartner.IsDeleted = false;
                }
                else
                {
                    throw new ArgumentException($"Partner with name `{partnerName}` already exist!");
                }
            }
            else
            {
                newPartner = new Partner
                {
                    Name = partnerName,
                    Address = address,
                    VAT = vat,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    PastOrders = new List<Order>()
                };
                this.context.Partners.Add(newPartner);
            }

            await this.context.SaveChangesAsync();
            return newPartner;
        }

        public async Task<Partner> EditAsync(string partnerName, string newPartnerName, string newVat = null)
        {
            Partner partnerToEdit =await this.context.Partners.FirstOrDefaultAsync(p => p.Name == partnerName);

            if (partnerToEdit == null)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            partnerToEdit.Name = newPartnerName;

            if (newVat != null)
            {
                partnerToEdit.VAT = newVat;
            }
            partnerToEdit.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return partnerToEdit;
        }

        public async Task<Partner> DeleteAsync(string partnerName)
        {
            Partner partnerToDelete =await this.context.Partners.FirstOrDefaultAsync(p => p.Name == partnerName);

            if (partnerToDelete == null || partnerToDelete.IsDeleted)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            partnerToDelete.IsDeleted = true;
            partnerToDelete.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return partnerToDelete;
        }

        public async Task<Partner> FindByNameAsync(string partnerName)
        {
            var partner = await this.context.Partners
                .Include(p => p.PastOrders)
                .FirstOrDefaultAsync(p => p.Name == partnerName);
            if (partner == null || partner.IsDeleted)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            return partner;
        }
        public async Task<Partner> FindByIdAsync(int id)
        {
            var partner = await this.context.Partners
                .Include(p => p.PastOrders)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (partner == null || partner.IsDeleted)
            {
                throw new ArgumentException($"Partner doesn't exist!");
            }

            return partner;
        }
    }
}
