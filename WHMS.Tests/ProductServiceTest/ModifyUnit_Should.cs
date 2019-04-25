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
    public class ModifyUnit_Should
    {
        [TestMethod]
        public void Should_Modify_Product_Unit()
        {
            var dbName = nameof(Should_Modify_Product_Unit);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(new Product() { Name = "Name", Unit = new Unit() { UnitName = "unit" } });
                arrangeContext.Units.Add(new Unit() { UnitName = "newUnit" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var unit = assertContext.Units.First(u => u.UnitName == "newUnit");
                var product = assertContext.Products.First(u => u.Name == "Name");
                sut.ModifyUnit(product, unit);

                Assert.AreEqual("Name", product.Name);
                Assert.IsNotNull(product);
                Assert.IsInstanceOfType(product, typeof(Product));
                Assert.AreEqual("newUnit", product.Unit.UnitName);

            }
        }
    }
}
