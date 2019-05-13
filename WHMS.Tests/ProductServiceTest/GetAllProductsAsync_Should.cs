using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class GetAllProductsAsync_Should
    {
        [TestMethod]
        public async Task Succeed()
        {
            //Arrange
            var dbName = ((nameof(GetAllProductsAsync_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            List<Product> allProducts;

            using (var assertContext = new ApplicationDbContext(options))
            {
                assertContext.Products.Add(new Product { Name = "pr1" });
                assertContext.Products.Add(new Product { Name = "pr2" });
                assertContext.Products.Add(new Product { Name = "pr3" });

                assertContext.SaveChanges();
            }

            //Act
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                allProducts = await sut.GetAllProductsAsync();
            }

            //Assert
            Assert.AreEqual(3, allProducts.Count);
            Assert.AreEqual("pr1", allProducts[0].Name);
            Assert.AreEqual("pr2", allProducts[1].Name);
            Assert.AreEqual("pr3", allProducts[2].Name);
        }
    }
}
