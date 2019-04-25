using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class GetProductById_Should
    {
        [TestMethod]
        public void Should_Throw_Exception_If_Null_ByID()
        {
            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Null_ByID))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetProductById(1));
                string expected = "Product does not exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Deleted_ByID()
        {
            var dbName = nameof(Should_Throw_Exception_If_Product_Deleted_ByID);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(new Product() { Name = "productName", IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetProductById(1));
                string expected = "Product does not exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Return_Product_ByID()
        {
            var dbName = nameof(Should_Return_Product_ByID);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(new Product() { Name = "productName" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var product = sut.GetProductById(1);
                Assert.IsNotNull(product);
                Assert.IsInstanceOfType(product, typeof(Product));
                Assert.AreEqual("productName", product.Name);
            }
        }
    }
}
