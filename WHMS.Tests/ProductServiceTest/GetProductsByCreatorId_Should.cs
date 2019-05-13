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
    public class GetProductsByCreatorId_Should
    {
        [TestMethod]
        public async Task Succeed()
        {
            var dbName = ((nameof(GetProductsByCreatorId_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            var category = new Category();
            var creator1 = new ApplicationUser();
            var creator2 = new ApplicationUser();
            var creator1Id = creator1.Id;
            var creator2Id = creator2.Id;
            var opw = new List<OrderProductWarehouse> { new OrderProductWarehouse() };
            var unit = new Unit();
            var whs = new List<ProductWarehouse>();
            var product1 = new Product { Category = category, Creator = creator1, Name = "product1", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };
            var product2 = new Product { Category = category, Creator = creator1, Name = "product2", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };
            var product3 = new Product { Category = category, Creator = creator2, Name = "product3", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };

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
                var products = await sut.GetProductsByCreatorId(creator1Id);
                Assert.AreEqual(2,products.Count);
                Assert.IsInstanceOfType(products, typeof(List<Product>));
                Assert.AreEqual("product1", products[0].Name);
                Assert.AreEqual("product2", products[1].Name);
            }
        }
    }
}
