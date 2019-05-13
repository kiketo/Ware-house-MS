using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class GetProductsByNameAsync_Should
    {
        [TestMethod]
        public async Task Throw_ExceptionIfNoProducts()
        {
            var dbName = ((nameof(GetProductsByNameAsync_Should)) + (nameof(Throw_ExceptionIfNoProducts)));
            var options = TestUtils.GetOptions(dbName);

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetProductsByNameAsync("productName"));
                string expected = "Product `productName` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public async Task Succeed()
        {
            var dbName = ((nameof(GetProductsByNameAsync_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);

            var category = new Category();
            var creator1 = new ApplicationUser();
            var creator1Id = creator1.Id;
            var opw = new List<OrderProductWarehouse> { new OrderProductWarehouse() };
            var unit = new Unit();
            var whs = new List<ProductWarehouse>();
            var product1 = new Product { Category = category, Creator = creator1, Name = "productName", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };
            var product2 = new Product { Category = category, Creator = creator1, Name = "productName", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };
            var product3 = new Product { Category = category, Creator = creator1, Name = "otherName", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(product1);
                arrangeContext.Products.Add(product2);
                arrangeContext.Products.Add(product3);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var products = await sut.GetProductsByNameAsync("productName");
                Assert.AreEqual(2,products.Count);
                Assert.IsInstanceOfType(products, typeof(List<Product>));
            }
        }
    }
}
