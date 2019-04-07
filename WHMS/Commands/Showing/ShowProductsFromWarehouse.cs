using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Showing
{
    public class ShowProductsFromWarehouse :ICommand
    {
        IProductService productService;
        IWarehouseService warehouseService;
        IProductWarehouseService productWarehouseService;
        public ShowProductsFromWarehouse(IProductService productService, IWarehouseService warehouseService, IProductWarehouseService productWarehouseService)
        {
            this.productService = productService;
            this.warehouseService = warehouseService;
            this.productWarehouseService = productWarehouseService;
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            var warehouse = this.warehouseService.GetByName(parameters[0]);
            if (warehouse == null)
            {
                throw new ArgumentException($"There is no warehouse with name {parameters[0]}");
            }
            var productList =  this.productWarehouseService.GetAllProductsInWarehouseWithQuantityOverZero(warehouse.Id);

            if (productList.Count == 0)
            {
                return $"There are no products stored in {warehouse.Name}";
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Product Name\tQuantity In {warehouse.Name}");
                foreach (var pw in productList)
                {
                    sb.Append(this.productService.GetProductById(pw.ProductId).Name);
                    sb.Append("\t");
                    sb.Append(pw.Quantity);
                    sb.AppendLine();
                }
                sb.AppendLine();
                return sb.ToString();
            }
        }
    }
}
