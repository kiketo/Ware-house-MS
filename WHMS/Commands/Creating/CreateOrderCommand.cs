using System;
using System.Collections.Generic;
using WHMS.Commands.Contracts;
using WHMS.Core.Contracts;
using WHMS.Services.Contracts;
using WHMS.Utils;
using WHMSData.Utills;

namespace WHMS.Commands.Creating
{
    public class CreateOrderCommand : ICommand
    {
        private IPDFExporter pDFExporter;
        private IWriter writer;
        private IReader reader;
        private IOrderService orderService;
        private IProductService productService;
        private IPartnerService partnerService;
        private IProductWarehouseService productWarehouseService;
        private IWarehouseService warehouseService;

        public CreateOrderCommand(IPDFExporter pDFExporter, IWriter writer, IReader reader, IOrderService orderService, IProductService productService, IPartnerService partnerService, IProductWarehouseService productWarehouseService, IWarehouseService warehouseService)
        {
            this.pDFExporter = pDFExporter ?? throw new ArgumentNullException(nameof(pDFExporter));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
            this.productWarehouseService = productWarehouseService ?? throw new ArgumentNullException(nameof(productWarehouseService));
            this.warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
        }

        public string Execute(IReadOnlyList<string> parameters) //type,partner,product,quantity,warehouse, comment
        {
            if (parameters.Count!=5&&parameters.Count!=6)
            {
                throw new ArgumentException(@"Please provide parameters: {type};{partner};{product};{quantity};{warehouse};[comment]");
            }
            OrderType orderType;
            if (!Enum.TryParse(parameters[0].ToLower(), true, out orderType))
            {
                throw new ArgumentException($"Order type '{parameters[0]}' is not valid. Should be 'buy' or 'sell'.");
            }
            var partner = this.partnerService.FindByName(parameters[1]);

            var product = this.productService.FindByName(parameters[2]);
            int quantity;
            if (!int.TryParse(parameters[3], out quantity))
            {
                throw new ArgumentException($"{parameters[3]} is not a valid quantity.");
            }

            var warehouse = this.warehouseService.GetByName(parameters[4]);
            var inStock = this.productWarehouseService.GetQuantity(product.Id, warehouse.Id);
            if (orderType.Equals("sell"))
            {
                if (inStock < quantity)
                {
                    throw new ArgumentException($"The wanted quantity of {product.Name} is not available in {warehouse.Name}.");
                }
                this.productWarehouseService.SubstractQuantity(product.Id, warehouse.Id, quantity);
            }
            else
            {
                this.productWarehouseService.AddQuantity(product.Id, warehouse.Id, quantity);
            }
            string comment=null;
            if (parameters.Count == 6)
            {
                comment = parameters[5];
            }
            var order = this.orderService.Add(orderType, partner, product, quantity, comment);
            partner.PastOrders.Add(order);

            this.writer.WriteLine("Do you want to print the order to pdf? [Y/N]");

            var choice = this.reader.ReadKey();
            this.writer.WriteLine();
            while (true)
            {
                if (choice.KeyChar == 'y')
                {
                    this.writer.WriteLine($"Order with ID: {order.Id} was created. Please specify a name for your pdf:");
                    var pdfName = this.reader.ReadLine();
                    pDFExporter.Export(order,pdfName);

                    return $"{pdfName}.pdf was created.";
                }
                else if (choice.KeyChar == 'n')
                {
                    return $"Order with ID: {order.Id} was created.";
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
