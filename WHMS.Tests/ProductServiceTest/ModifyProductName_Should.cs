using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class ModifyProductName_Should
    {
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Is_Null()
        {
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Is_Null))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.ModifyProductName("oldName", "newName"));
                string expected = "Product oldName does not exists";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Is_Deleted()
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Is_Deleted))))
            {
                arrangeContext.Products.Add(new Product() { Name = "oldName", IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Is_Deleted))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.ModifyProductName("oldName", "newName"));
                string expected = "Product oldName does not exists";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Modify_Product_Name()
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Modify_Product_Name))))
            {
                arrangeContext.Products.Add(new Product() { Name = "oldName" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Modify_Product_Name))))
            {
                var sut = new ProductService(assertContext);
                var product = sut.ModifyProductName("oldName", "newName");
                
                Assert.IsNotNull(product);
                Assert.AreEqual("newName", product.Name);
            }
        }
    }
}
