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
    public class ModifyCategory_Should
    {
        [TestMethod]
        public void Should_Modify_Product_Category()
        {
            var dbName = nameof(Should_Modify_Product_Category);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(new Product() { Name = "Name", Category = new Category() { Name = "CATEGORY" } });
                arrangeContext.Categories.Add(new Category() { Name = "newCATEGORY" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var category = assertContext.Categories.First(u => u.Name == "newCATEGORY");
                var product = assertContext.Products.First(u => u.Name == "Name");
                sut.ModifyCategory(product, category);

                Assert.AreEqual("Name", product.Name);
                Assert.IsNotNull(product);
                Assert.IsInstanceOfType(product, typeof(Product));
                Assert.AreEqual("newCATEGORY", product.Category.Name);

            }
        }
    }
}
