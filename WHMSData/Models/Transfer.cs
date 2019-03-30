using System.Collections.Generic;
using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Transfer : Model
    {
        //public int FromWarehouseId { get; set; }  //TODO

        public Warehouse FromWarehouse { get; set; }

        //public int ToWarehouseId { get; set; } //TODO

        public Warehouse ToWarehouse { get; set; }

        public ICollection<Product> Products { get; set; }

        public string Comments { get; set; }
    }
}
