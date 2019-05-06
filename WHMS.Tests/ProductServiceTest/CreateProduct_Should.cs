//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using WHMSData.Context;
//using WHMSData.Models;

//namespace WHMS.Services.Tests.ProductServiceTest
//{
//    [TestClass]
//    public class CreateProduct_Should
//    {
//        [TestMethod]
//        public void Throw_Exception_If_Product_Exists()
//        {
//            var dbName = nameof(Throw_Exception_If_Product_Exists);

//            var options = TestUtils.GetOptions(dbName);
//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Products.Add(new Product() { Name = "ProductName" });
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => (sut.CreateProduct("ProductName", null, null, 0, 0, null)));
//                var expectedMessage = $"Product ProductName already exists";
//                Assert.AreEqual(expectedMessage, ex.Message);
//            }
//        }
//        [TestMethod]
//        [DataRow(-40.0424322)]
//        [DataRow(-32432.21)]
//        [DataRow(-3)]
//        [DataRow(-0.21)]

//        public void Throw_Exception_If_Margin_Less_Than_Zero(double margin)
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Throw_Exception_If_Margin_Less_Than_Zero))))
//            {
//                var sut = new ProductService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => (sut.CreateProduct("ProductName", null, null, 0, margin, null)));
//                var expectedMessage = $"The price margin cannot be negative number";
//                Assert.AreEqual(expectedMessage, ex.Message);
//            }
//        }
//        [TestMethod]
//        [DataRow("-40.0424322")]
//        [DataRow("-324.65464521")]
//        [DataRow("-3")]
//        [DataRow("-0.21")]
//        public void Throw_Exception_If_Product_Buy_Price_Is_Less_Than_Zero(string number)
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Throw_Exception_If_Product_Buy_Price_Is_Less_Than_Zero))))
//            {
//                var sut = new ProductService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => (sut.CreateProduct("ProductName", null, null,decimal.Parse(number), 0, null)));
//                var expectedMessage = $"Price cannot be negative number";
//                Assert.AreEqual(expectedMessage, ex.Message);
//            }
//        }
//        [TestMethod]
//        public void Properly_Set_SellPrice_On_Product_Creation()
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Properly_Set_SellPrice_On_Product_Creation))))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.CreateProduct("ProductName", null, null, 5, 20, null);
//                assertContext.SaveChanges();
//                Assert.AreEqual(6,product.SellPrice);
//            }
//        }
//        [TestMethod]
//        public void Properly_Set_BuyPrice_On_Product_Creation()
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Properly_Set_BuyPrice_On_Product_Creation))))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.CreateProduct("ProductName", null, null, 5, 0, null);
//                assertContext.SaveChanges();
//                Assert.AreEqual(5, product.BuyPrice);
//            }
//        }
//        [TestMethod]
//        public void Properly_Set_Margin_On_Product_Creation()
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Properly_Set_Margin_On_Product_Creation))))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.CreateProduct("ProductName", null, null, 0, 30, null);
//                assertContext.SaveChanges();
//                Assert.AreEqual(30, product.MarginInPercent);
//            }
//        }
//        [TestMethod]
//        public void Properly_Set_Description_On_Product_Creation()
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Properly_Set_Description_On_Product_Creation))))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.CreateProduct("ProductName", null, null, 0, 0, "Goody two shoes Description");
//                assertContext.SaveChanges();
//                Assert.AreEqual("Goody two shoes Description", product.Description);
//            }
//        }
//        [TestMethod]
//        public void Properly_Set_Existing_Category_On_Product_Creation()
//        {
//            var dbName = nameof(Properly_Set_Existing_Category_On_Product_Creation);

//            var options = TestUtils.GetOptions(dbName);

//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Categories.Add(new Category() { Name = "CategoryName" });
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var category = assertContext.Categories.Where(n => n.Name == "CategoryName").FirstOrDefault();
//                var product = sut.CreateProduct("ProductName", null, category, 0, 0, null);
//                assertContext.SaveChanges();
//                Assert.AreEqual("CategoryName", product.Category.Name);
//            }
//        }
//        [TestMethod]
//        public void Properly_Set_Existing_Unit_On_Product_Creation()
//        {
//            var dbName = nameof(Properly_Set_Existing_Category_On_Product_Creation);

//            var options = TestUtils.GetOptions(dbName);

//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Units.Add(new Unit() { UnitName = "UnitName" });
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var unit = assertContext.Units.Where(n => n.UnitName == "UnitName").FirstOrDefault();
//                var product = sut.CreateProduct("ProductName", unit, null, 0, 0, null);
//                assertContext.SaveChanges();
//                Assert.AreEqual("UnitName", product.Unit.UnitName);
//            }
//        }
//        [TestMethod]
//        public void Create_Product()
//        {
           
//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Create_Product))))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.CreateProduct("ProductName", null, null, 0, 0, null);
//                assertContext.SaveChanges();

//                Assert.IsNotNull(product);
//                Assert.AreEqual("ProductName", product.Name);
//            }
//        }

//    }
//}
