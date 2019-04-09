using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class FindByName_Should
    {
        [TestMethod]
        public void Should_Throw_Exception_If_Null()
        {
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Null))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.FindByName("productName"));
                string expected = "Product `productName` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Deleted()
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Deleted))))
            {
                arrangeContext.Products.Add(new Product() { Name = "productName", IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Deleted))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.FindByName("productName"));
                string expected = "Product `productName` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Return_Product_F()
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Return_Product_F))))
            {
                arrangeContext.Products.Add(new Product() { Name = "productName"});
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Return_Product_F))))
            {
                var sut = new ProductService(assertContext);
                var product =  sut.FindByName("productName");
                Assert.IsNotNull(product);
                Assert.IsInstanceOfType(product, typeof(Product));
                Assert.AreEqual("productName", product.Name);
            }
        }
    }
}
