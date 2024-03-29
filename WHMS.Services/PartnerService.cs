﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Services.Contracts;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly WHMSContext context;

        public PartnerService(WHMSContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Partner Add(string partnerName, Address address = null, string vat = null)
        {
            Partner newPartner = this.context.Partners.FirstOrDefault(t => t.Name == partnerName);

            if (newPartner != null)
            {
                if (newPartner.IsDeleted)
                {
                    newPartner.Name = partnerName;
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

            this.context.SaveChanges();
            return newPartner;
        }

        public Partner Edit(string partnerName, string newPartnerName, string newVat = null)
        {
            Partner partnerToEdit = this.context.Partners.FirstOrDefault(p => p.Name == partnerName);

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

            this.context.SaveChanges();
            return partnerToEdit;
        }

        public Partner Delete(string partnerName)
        {
            Partner partnerToDelete = this.context.Partners.FirstOrDefault(p => p.Name == partnerName);

            if (partnerToDelete == null || partnerToDelete.IsDeleted)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            partnerToDelete.IsDeleted = true;
            partnerToDelete.ModifiedOn = DateTime.Now;

            this.context.SaveChanges();
            return partnerToDelete;
        }

        public Partner FindByName(string partnerName)
        {
            var partner = this.context.Partners
                .Include(p => p.PastOrders)
                .FirstOrDefault(p => p.Name == partnerName);
            if (partner == null || partner.IsDeleted)
            {
                throw new ArgumentException($"Partner with name `{partnerName}` doesn't exist!");
            }

            return partner;
        }
    }
}
