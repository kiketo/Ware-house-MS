using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class CreateProductAsync_Should
    {
        [TestMethod]
        [DataRow(-40.0424322)]
        [DataRow(-324.65464521)]
        [DataRow(-3)]
        [DataRow(-0.21)]
        public async Task ThrowException_IfNegativeMargin(double margin)
        {
            //Arrange
            var dbName = ((nameof(CreateProductAsync_Should)) + (nameof(ThrowException_IfNegativeMargin)));
            var options = TestUtils.GetOptions(dbName);

            //Act&Assert
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => ( sut.CreateProductAsync("ProductName", null, null, 0, margin, null, TestUtils.User())));
                var expectedMessage = "The price margin cannot be negative number";
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        [DataRow("-40.0424322")]
        [DataRow("-324.65464521")]
        [DataRow("-3")]
        [DataRow("-0.21")]
        public async Task ThrowException_IfNegativeBuyPrice(string number)
        {
            //Arrange
            var dbName = ((nameof(CreateProductAsync_Should)) + (nameof(ThrowException_IfNegativeBuyPrice)));
            var options = TestUtils.GetOptions(dbName);

            //Act&Assert
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var ex =await Assert.ThrowsExceptionAsync<ArgumentException>(() => (sut.CreateProductAsync("ProductName", null, null, decimal.Parse(number), 0, null, TestUtils.User())));
                var expectedMessage = $"Price cannot be negative number";
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        public async Task ProperlySetSellPrice()
        {
            //Arrange
            var dbName = ((nameof(CreateProductAsync_Should)) + (nameof(ProperlySetSellPrice)));
            var options = TestUtils.GetOptions(dbName);
            decimal buyPrice = 5;
            double margin = 20;
            decimal expectedSellPrice = buyPrice * (1+ (decimal)margin / 100);

            //Act&Assert
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var product = await sut.CreateProductAsync("ProductName", null, null, buyPrice, margin, null, TestUtils.User());
                assertContext.SaveChanges();
                Assert.AreEqual(expectedSellPrice, product.SellPrice);
            }
        }

        [TestMethod]
        public async Task ProperlySet_Name_BuyPrice_Margin_Description()
        {
            var dbName = ((nameof(CreateProductAsync_Should)) + (nameof(ProperlySetSellPrice)));
            var options = TestUtils.GetOptions(dbName);

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var product = await sut.CreateProductAsync("ProductName", null, null, 5, 20, "description", TestUtils.User());
                assertContext.SaveChanges();
                Assert.AreEqual(5, product.BuyPrice);
                Assert.AreEqual(20, product.MarginInPercent);
                Assert.AreEqual("description", product.Description);
                Assert.AreEqual("ProductName", product.Name);
            }
        }

        [TestMethod]
        public async Task ProperlySet_ExistingCategory()
        {
            var dbName = ((nameof(CreateProductAsync_Should)) + (nameof(ProperlySet_ExistingCategory)));
            var options = TestUtils.GetOptions(dbName);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Categories.Add(new Category() { Name = "CategoryName" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var category = assertContext.Categories.Where(n => n.Name == "CategoryName").FirstOrDefault();
                var product =await sut.CreateProductAsync("ProductName", null, category, 0, 0, null,TestUtils.User());
                assertContext.SaveChanges();
                Assert.AreEqual("CategoryName", product.Category.Name);
            }
        }

        [TestMethod]
        public async Task ProperlySet_ExistingUnit()
        {
            var dbName = ((nameof(CreateProductAsync_Should)) + (nameof(ProperlySet_ExistingUnit)));
            var options = TestUtils.GetOptions(dbName);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Units.Add(new Unit() { UnitName = "UnitName" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var unit = assertContext.Units.Where(n => n.UnitName == "UnitName").FirstOrDefault();
                var product =await sut.CreateProductAsync("ProductName", unit, null, 0, 0, null,TestUtils.User());
                assertContext.SaveChanges();
                Assert.AreEqual("UnitName", product.Unit.UnitName);
            }
        }
    }
}
