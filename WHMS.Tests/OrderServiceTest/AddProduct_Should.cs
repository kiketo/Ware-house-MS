using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class AddProduct_Should
    {
        [TestMethod]
        public async Task Succeed()
        {
            //Arrange
            var dbName = ((nameof(AddProduct_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            Product product2;
            Unit unit = new Unit { Id = 1, UnitName = "pc" };
            Partner partner = new Partner { Id = 1, Name = "Partner1" };
            Product product1 = new Product { Id = 1, Unit = unit, Name = "Product1", MarginInPercent = 10 };
            product2 = new Product { Id = 2, Unit = unit, Name = "Product2", MarginInPercent = 20 };
            //arrangeContext.SaveChanges();
            ProductWarehouse pw = new ProductWarehouse() { Product = product1 };
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Partners.Add(partner);
                arrangeContext.Products.Add(product1);
                arrangeContext.Products.Add(product2);
                arrangeContext.Units.Add(unit);
                arrangeContext.Orders.Add(new Order { Id = 1, ProductsWarehouses = new List<ProductWarehouse> { pw }, Partner = partner });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                ProductWarehouse pw1 = new ProductWarehouse() { Product = product2 };
                var sut = new OrderService(assertContext);
                var addProduct = await sut.AddProductToOrderAsync(1, pw1);
                Assert.IsTrue(addProduct.ProductsWarehouses.Contains(pw1));
            }
        }//TODO
    }
}
