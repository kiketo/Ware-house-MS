using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class DeleteProduct_Should
    {
        [TestMethod]
        public void Should_Throw_Exception_If_Null()
        {
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Null))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.DeleteProduct("ProductName"));
                string expected = "Product `ProductName` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Is_Deleted()
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Is_Deleted))))
            {
                arrangeContext.Products.Add(new Product() { Name = "ProductName", IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Is_Deleted))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.DeleteProduct("ProductName"));
                string expected = "Product `ProductName` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Delete_Product()
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Delete_Product))))
            {
                arrangeContext.Products.Add(new Product() { Name = "ProductName", IsDeleted = false });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Delete_Product))))
            {
                var sut = new ProductService(assertContext);
                var product = sut.DeleteProduct("ProductName");
                
                Assert.IsTrue(product.IsDeleted ==true);
                Assert.IsNotNull(product);
            }
        }
    }
}
