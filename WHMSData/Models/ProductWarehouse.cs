using Newtonsoft.Json;
using System.Collections.Generic;

namespace WHMSData.Models
{
    public class ProductWarehouse
    {
        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public int WarehouseId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        [JsonIgnore]
        public Warehouse Warehouse { get; set; }

        public int Quantity { get; set; }
    }
}