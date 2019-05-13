using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class GetProductByIdAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_IfProductIsNull()
        {
            var dbName = ((nameof(GetProductByIdAsync_Should)) + (nameof(ThrowException_IfProductIsNull)));
            var options = TestUtils.GetOptions(dbName);

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetProductByIdAsync(1));
                string expected = "Product does not exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        public async Task Succeed()
        {
            var dbName = ((nameof(GetProductByIdAsync_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            var category = new Category();
            var creator = new ApplicationUser();
            var opw = new List<OrderProductWarehouse> { new OrderProductWarehouse() };
            var unit = new Unit();
            var whs = new List<ProductWarehouse>();
            var product1 = new Product {Category=category,Creator=creator,Name="product1",OrderProductWarehouses=opw,Unit=unit,Warehouses=whs };
            int id = 2;
            var product2 = new Product {Category=category,Creator=creator,Name="product2",OrderProductWarehouses=opw,Unit=unit,Warehouses=whs };

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(product1);
                arrangeContext.Products.Add(product2);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var product = await sut.GetProductByIdAsync(id);
                Assert.IsNotNull(product);
                Assert.IsInstanceOfType(product, typeof(Product));
                Assert.AreEqual("product2", product.Name);
            }
        }
    }
}
