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

        public async Task<Partner> AddAsync(string partnerName, ApplicationUser user, Address address = null, string vat = null)
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
                    newPartner.Creator = user;
                }
                else
                {
                    return newPartner;
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
                    PastOrders = new List<Order>(),
                    Creator = user
                };
                this.context.Partners.Add(newPartner);
            }

            await this.context.SaveChangesAsync();
            return newPartner;
        }

        public async Task<Partner> UpdateAsync(Partner partner)
        {
            this.context.Attach(partner).State =
                Microsoft.EntityFrameworkCore
                .EntityState.Modified;
            await this.context.SaveChangesAsync();
            return partner;
        }

        public async Task<Partner> DeleteAsync(int id)
        {
            Partner partnerToDelete = await this.context.Partners.FirstOrDefaultAsync(p => p.Id == id);

            if (partnerToDelete == null || partnerToDelete.IsDeleted)
            {
                throw new ArgumentException($"Partner with ID `{id}` doesn't exist!");
            }

            partnerToDelete.IsDeleted = true;
            partnerToDelete.ModifiedOn = DateTime.Now;

            await this.context.SaveChangesAsync();
            return partnerToDelete;
        }

        public async Task<Partner> GetByNameAsync(string partnerName)
        {
            var partner = await this.context.Partners
                .Where(p => p.Name == partnerName && !p.IsDeleted)
                .Include(p => p.PastOrders)
                .Include(p => p.Address)
                    .ThenInclude(a => a.Town)
                .Include(p => p.Creator)
                .FirstOrDefaultAsync();

            if (partner == null)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            return partner;
        }

        public async Task<Partner> GetByIdAsync(int Id)
        {
            var partner = await this.context.Partners
                .Where(p => p.Id == Id)
                .Include(p => p.Creator)
                .Include(p => p.PastOrders)
                .Include(a => a.Address)
                    .ThenInclude(t => t.Town)
                .FirstOrDefaultAsync();

            if (partner == null || partner.IsDeleted)
            {
                throw new ArgumentException($"Partner with ID `{Id}` doesn't exist!");
            }

            return partner;
        }

        public async Task<Partner> GetByVATAsync(string VAT)
        {
            var partner = await this.context.Partners
                .Where(p => p.VAT == VAT)
                .Include(p => p.Creator)
                .Include(p => p.PastOrders)
                .Include(a => a.Address)
                    .ThenInclude(t => t.Town)
                .FirstOrDefaultAsync();

            if (partner == null || partner.IsDeleted)
            {
                throw new ArgumentException($"Partner with VAT `{VAT}` doesn't exist!");
            }

            return partner;
        }

        public Task<List<Partner>> GetAllPartners()
        {
            var partners = this.context.Partners
                .Where(p => p.IsDeleted == false)
                .ToListAsync();

            return partners;
        }
    }
}
