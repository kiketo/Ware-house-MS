using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class ProductsByCategory_Should
    {
        [TestMethod]
        public void Should_Return_Collection_Of_Products_In_Category()
        {
            var dbName = nameof(Should_Return_Collection_Of_Products_In_Category);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new WHMSContext(options))
            {
                arrangeContext.Categories.Add(new Category() { Name = "categoryName" });
                arrangeContext.SaveChanges();
                var category = arrangeContext.Categories.First(c => c.Name == "categoryName");
                arrangeContext.Products.Add(new Product() { Name = "product1", Category = category });
                arrangeContext.Products.Add(new Product() { Name = "product2", Category = null });
                arrangeContext.Products.Add(new Product() { Name = "product3", Category = category });
                arrangeContext.Products.Add(new Product() { Name = "product4", Category = category });

                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(options))
            {
                var sut = new ProductService(assertContext);
                var category = assertContext.Categories.First(c => c.Name == "categoryName");
                var products =  sut.ProductsByCategory(category);
                Assert.AreEqual(3, products.Count);
                Assert.IsNotNull(products);
                Assert.IsTrue(products.Any(n=>n.Name == "product1"));
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Null_Category()
        {
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Null_Category))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.ProductsByCategory(null));
                string expected = "Category does not exists";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Category_Deleted()
        {
            var dbName = nameof(Should_Throw_Exception_If_Category_Deleted);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new WHMSContext(options))
            {
                arrangeContext.Categories.Add(new Category() { Name = "categoryName", IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(options))
            {
                var sut = new ProductService(assertContext);
                var category = assertContext.Categories.FirstOrDefault(c => c.Name == "categoryName");
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.ProductsByCategory(category));
                string expected = "Category does not exists";
                Assert.AreEqual(expected, ex.Message);
            }
        }
    }
}
