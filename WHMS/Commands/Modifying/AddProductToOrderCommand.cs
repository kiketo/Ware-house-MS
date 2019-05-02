using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Modifying
{
    public class AddProductToOrderCommand : ICommand
    {
        private IOrderService orderService;
        private IProductService productService;
        private IProductWarehouseService productWarehouseService;
        private IWarehouseService warehouseService;

        public AddProductToOrderCommand(
            IOrderService orderService,
            IProductService productService,
            IProductWarehouseService productWarehouseService,
            IWarehouseService warehouseService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.productWarehouseService = productWarehouseService;
            this.warehouseService = warehouseService;
        }

        //orderId,product,quantity,warehouse
        public async Task<string> Execute(IReadOnlyList<string> parameters)
        {
            if (parameters.Count!=4)
            {
                throw new ArgumentException(@"Please provide parameters: OrderId;Product;Quantity;Warehouse");
            }
            int orderId;
            if (!int.TryParse(parameters[0], out orderId))
            {
                throw new ArgumentException($"{parameters[0]} is not a valid order ID.");
            }

            var order = await orderService.GetOrderByIdAsync(orderId);
            var product = this.productService.FindByName(parameters[1]);
            int quantity;
            if (!int.TryParse(parameters[2], out quantity))
            {
                throw new ArgumentException($"{parameters[2]} is not a valid quantity.");
            }

            var warehouse = this.warehouseService.GetByName(parameters[3]);
            var inStock = await this.productWarehouseService.GetQuantityAsync(product.Id, warehouse.Id);
            if (order.Type.Equals("sell"))
            {
                if (inStock < quantity)
                {
                    throw new ArgumentException($"The wanted quantity of {product.Name} is not available in {warehouse.Name}");
                }
                await this.productWarehouseService.SubstractQuantityAsync(product.Id, warehouse.Id, quantity);
            }
            else
            {
                await this.productWarehouseService.AddQuantityAsync(product.Id, warehouse.Id, quantity);
            }

           // await orderService.AddProductToOrderAsync(orderId, product, quantity);


            return $"Product: {product.Name} was added to Order with ID: {order.Id}.";
        }
    }
}
