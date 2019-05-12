﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMSWebApp2.Models
{
    public class OrderViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "ID should be a positive number")]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Type is required!")]
        public string Type { get; set; }

        [Required]
        public int PartnerId { get; set; }

        [MinLength(0, ErrorMessage = "Partner is required!")]
        public string Partner { get; set; }

        public IEnumerable<Product> Products { get; set; } //TODO string

        public string Comment { get; set; }

        public decimal TotalValue { get; set; }

        public string CreatorId { get; set; }

        public string Creator { get; set; }

        public bool CanUserEdit { get; set; }

        public bool CanUserDelete { get; set; }

        public int Quantity { get; set; }

        public string Warehouse { get; set; }

        public IReadOnlyCollection<OrderViewModel> SearchResults { get; set; } = new List<OrderViewModel>();


        public IOrderedEnumerable<SelectListItem> ProductsInWarehouse { get; set; }

        public IOrderedEnumerable<SelectListItem> Warehouses { get; set; }

        public IOrderedEnumerable<SelectListItem> Partners { get; set; }

        public IOrderedEnumerable<SelectListItem> ProductsList { get; set; }

        public OrderType TypeOrder { get; set; }

        public Dictionary<ProductWarehouse, int> ProductsQuantities { get; set; }
                
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int WarehouseId { get; set; }

        public string ProductWarehouse { get; set; }

        public Dictionary<ProductWarehouse, int> WantedQuantityByProduct { get; set; }

        public Dictionary<Product, int> ProductsQuantity { get; set; }

        public List<OrderProductViewModel> listProductsWithQuantities { get; set; }

        public List<OrderProductViewModel> SelectedProductsWithQuantities { get; set; }

        public List<OrderProductViewModel> List2ProductsWithQuantities { get; set; }

        public MultiSelectList ProductWQQuantities { get; set; }

        public ICollection<OrderProductWarehouse> ProductsQuantitiesOPW { get; set; }

        public int WantedQuantity { get; set; }

        public KeyValuePair<Product, int> SelectedProduct { get; set; }

        public int ProductId { get; set; }




    }
}