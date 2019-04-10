using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Core.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Deleting
{
    public class DeleteProductCommand : ICommand
    {
        IWriter writer;
        IReader reader;
        IProductService productService;
        IProductWarehouseService productWarehouse;
        IWarehouseService warehouseService;
        ICategoryService categoryService;
        public DeleteProductCommand(
            IProductService productService, 
            IProductWarehouseService productWarehouse, 
            IWarehouseService warehouseService,
             ICategoryService categoryService,
             IWriter writer,
             IReader reader
            )
        {
            this.productService = productService;
            this.productWarehouse = productWarehouse;
            this.warehouseService = warehouseService;
            this.categoryService = categoryService;
            this.writer = writer;
            this.reader = reader;
        }

        public string Execute(IReadOnlyList<string> parameters) //productname
        {
            var product = this.productService.FindByName(parameters[0]);
            var sb = new StringBuilder();
            sb.Append("product Name:\t");
            sb.AppendLine(product.Name);
            sb.Append("Category:\t");
            sb.AppendLine(this.categoryService.FindByName(product.Category.Name).Name);
            sb.Append("Product Description:\t");
            sb.AppendLine(product.Description);
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine("Do you want to delete the product? [Y/N]");
            
            var choice = this.reader.ReadKey();
            this.writer.WriteLine();
            while (true)
            {
                if (choice.KeyChar == 'y')
                {
                    var listWH = this.warehouseService.GetAllWarehouses();
                    foreach (var wh in listWH)
                    {
                        var quantity = this.productWarehouse.GetQuantity(product.Id, wh.Id);
                        if (quantity > 0)
                        {
                            throw new ArgumentException("The product is still in stock in some of the warehouses\n\rSo you cannot delete the product");
                        }
                    }

                    this.productService.DeleteProduct(parameters[0]);

                    return $"Product {parameters[0]} was deleted";
                }
                else if (choice.KeyChar == 'n')
                {
                    return $"Product {product.Name} was not deleted";
                }
                else
                {
                    this.writer.WriteLine("Enter Valid Key [Y/N]");
                    choice = this.reader.ReadKey();
                    this.writer.WriteLine();
                }
            }

        }

    }
}
