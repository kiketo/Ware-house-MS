using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class UndeleteProduct_Should
    {
        [TestMethod]
        public void Should_Return_Deleted_Product()
        {
            var dbName = nameof(Should_Return_Deleted_Product);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new WHMSContext(options))
            {
                arrangeContext.Products.Add(new Product() { Name = "Name", IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(options))
            {
                var sut = new ProductService(assertContext);
                var product = sut.UndeleteProduct("Name");

                Assert.AreEqual("Name", product.Name);
                Assert.IsNotNull(product);
                Assert.IsInstanceOfType(product, typeof(Product));
                Assert.IsTrue(product.IsDeleted == false);
            }
        }
    }
}
