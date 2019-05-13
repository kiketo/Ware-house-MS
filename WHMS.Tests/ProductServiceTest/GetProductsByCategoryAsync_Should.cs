using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class GetProductsByCategoryAsync_Should
    {
        [TestMethod]
        public async Task Succeed()
        {
            var dbName = ((nameof(GetProductsByNameAsync_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);

            var category = new Category {Name="cat" };
            var category1 = new Category {Name="other cat" };
            var creator1 = new ApplicationUser();
            var creator1Id = creator1.Id;
            var opw = new List<OrderProductWarehouse> { new OrderProductWarehouse() };
            var unit = new Unit();
            var whs = new List<ProductWarehouse>();
            var product1 = new Product { Category = category, Creator = creator1, Name = "productName", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };
            var product2 = new Product { Category = category, Creator = creator1, Name = "productName", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };
            var product3 = new Product { Category = category1, Creator = creator1, Name = "otherName", OrderProductWarehouses = opw, Unit = unit, Warehouses = whs };


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
                var products = await sut.GetProductsByCategoryAsync(category);
                Assert.AreEqual(2, products.Count);
                Assert.IsTrue(products.All(n => n.Category.Name == "cat"));
            }
        }

        [TestMethod]
        public async Task ThrowException_IfNullCategory()
        {
            var dbName = ((nameof(GetProductsByNameAsync_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetProductsByCategoryAsync(null));
                string expected = "Category does not exists";
                Assert.AreEqual(expected, ex.Message);
            }
        }
    }
}
