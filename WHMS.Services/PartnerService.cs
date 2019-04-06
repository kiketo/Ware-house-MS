using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Interfaces;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly WHMSContext context;
        private readonly ITownService townService;
        private readonly IAddressSevice addressSevice;

        public PartnerService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.addressSevice = addressSevice ?? throw new ArgumentNullException(nameof(addressSevice));
        }

        public Partner Add(string partnerName, int vat = 0)
        {
            if (this.context.Partners.Any(p => p.Name == partnerName))
            {
                throw new ArgumentException($"Partner with name `{partnerName}` already exist!");
            }
            
            Partner newPartner = new Partner
            {
                Name = partnerName,
                VAT = vat,
                CreatedOn = DateTime.Now,
                //PastOrders = new List<Order>()
            };

            this.context.Partners.Add(newPartner);
            this.context.SaveChanges();
            return newPartner;
        }

        public Partner Edit(string partnerName, string newPartnerName, int newVat=0)
        {
            Partner partnerToEdit = this.context.Partners.FirstOrDefault(p => p.Name == partnerName);

            if (partnerToEdit==null)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            partnerToEdit.Name = newPartnerName;

            if (newVat!=0)
            {
                partnerToEdit.VAT = newVat;
            }
            partnerToEdit.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return partnerToEdit;
        }

        public Partner Delete (string partnerName)
        {
            Partner partnerToDelete = this.context.Partners.FirstOrDefault(p => p.Name == partnerName);

            if (partnerToDelete == null)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            partnerToDelete.IsDeleted = true;
            partnerToDelete.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return partnerToDelete;
        }

        public Partner FindByName (string partnerName)
        {
            Partner partner = this.context.Partners.FirstOrDefault(p => p.Name == partnerName);
            if (partner==null)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            return partner;
        }
    }
}
