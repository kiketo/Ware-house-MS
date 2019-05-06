//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using WHMSData.Context;
//using WHMSData.Models;

//namespace WHMS.Services.Tests.ProductServiceTest
//{
//    [TestClass]
//    //public class SetBuyPrice_Should
//    {
//        [TestMethod]
//        public void Should_Throw_Exception_If_Product_Not_Found_Null_SP()
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Not_Found_Null_SP))))
//            {
//                var sut = new ProductService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => sut.SetBuyPrice(1,(decimal)5.0000212));
//                string expected = "Product does not exist!";
//                Assert.AreEqual(expected, ex.Message);
//            }
//        }
//        [TestMethod]
//        public void Should_Throw_Exception_If_Product_Is_Deleted_SP()
//        {
//            var dbName = nameof(Should_Throw_Exception_If_Product_Is_Deleted_SP);

//            var options = TestUtils.GetOptions(dbName);
//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Products.Add(new Product() { IsDeleted = true });
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => sut.SetBuyPrice(1, 43.432m ));
//                string expected = "Product does not exist!";
//                Assert.AreEqual(expected, ex.Message);
//            }
//        }
//        [TestMethod]
//        [DataRow("-40.0424322")]
//        [DataRow("-324.65464521")]
//        [DataRow("-3")]
//        [DataRow("-0.21")]
//        public void Throw_Exception_If_Product_Buy_Price_Is_Less_Than_Zero_SP(string number)
//        {
//            var dbName = nameof(Throw_Exception_If_Product_Buy_Price_Is_Less_Than_Zero_SP);

//            var options = TestUtils.GetOptions(dbName);
//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Products.Add(new Product() { Name = "Name", BuyPrice =0});
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => (sut.SetBuyPrice(1, decimal.Parse(number))));
//                var expectedMessage = $"Price cannot be negative number";
//                Assert.AreEqual(expectedMessage, ex.Message);
//            }
//        }
//        [TestMethod]
//        [DataRow("40.0424322")]
//        [DataRow("324.65464521")]
//        [DataRow("3")]
//        [DataRow("0.21")]
//        public void Should_Set_Buy_Price(string number)
//        {
//            var dbName = nameof(Should_Set_Buy_Price);

//            var options = TestUtils.GetOptions(dbName);
//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Products.Add(new Product() { Name = "Name", BuyPrice = 0 });
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var product=sut.SetBuyPrice(1, decimal.Parse(number));
                
//                Assert.AreEqual(decimal.Parse(number), product.BuyPrice);
//                Assert.IsNotNull(product);
//                Assert.IsInstanceOfType(product, typeof(Product));
//            }
//        }
//    }
//}
