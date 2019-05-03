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
            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Null))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.FindByNameAsync("productName"));
                string expected = "Product `productName` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Deleted()
        {
            var dbName = nameof(Should_Throw_Exception_If_Product_Deleted);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(new Product() { Name = "productName", IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.FindByNameAsync("productName"));
                string expected = "Product `productName` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        //[TestMethod]
        //public void Should_Return_Product_F()
        //{
        //    var dbName = nameof(Should_Return_Product_F);

        //    var options = TestUtils.GetOptions(dbName);
        //    using (var arrangeContext = new ApplicationDbContext(options))
        //    {
        //        arrangeContext.Products.Add(new Product() { Name = "productName"});
        //        arrangeContext.SaveChanges();
        //    }
        //    using (var assertContext = new ApplicationDbContext(options))
        //    {
        //        var sut = new ProductService(assertContext);
        //        var product =  sut.FindByNameAsync("productName");
        //        Assert.IsNotNull(product);
        //        Assert.IsInstanceOfType(product, typeof(Product));
        //        Assert.AreEqual("productName", product.Name);
        //    }
        //}
    }
}
