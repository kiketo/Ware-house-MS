//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using WHMSData.Context;
//using WHMSData.Models;

//namespace WHMS.Services.Tests.ProductServiceTest
//{
//    [TestClass]
//    public class FindByNameInclncludingDeleted_Should
//    {
//        [TestMethod]
//        public void Should_Not_Throw_Exception_If_Null()
//        {

//            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Should_Not_Throw_Exception_If_Null))))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.FindByNameInclncludingDeleted("productName");
//                Assert.IsNull(product);
                
//            }
//        }
//        [TestMethod]
//        public void Should_Find_Product_That_Is_Deleted()
//        {
//            var dbName = nameof(Should_Find_Product_That_Is_Deleted);

//            var options = TestUtils.GetOptions(dbName);
//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Products.Add(new Product() { Name = "productName", IsDeleted = true });
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.FindByNameInclncludingDeleted("productName");
//                Assert.IsNotNull(product);
//                Assert.IsInstanceOfType(product, typeof(Product));
//                Assert.AreEqual("productName", product.Name);
//                Assert.IsTrue(product.IsDeleted);
//            }
//        }
//        [TestMethod]

//        public void Should_Return_Product()
//        {
//            var dbName = nameof(Should_Return_Product);

//            var options = TestUtils.GetOptions(dbName);
//            using (var arrangeContext = new ApplicationDbContext(options))
//            {
//                arrangeContext.Products.Add(new Product() { Name = "productName", IsDeleted = true });
//                arrangeContext.SaveChanges();
//            }
//            using (var assertContext = new ApplicationDbContext(options))
//            {
//                var sut = new ProductService(assertContext);
//                var product = sut.FindByNameInclncludingDeleted("productName");
//                Assert.IsNotNull(product);
//                Assert.IsInstanceOfType(product, typeof(Product));
//                Assert.AreEqual("productName", product.Name);
//                Assert.IsTrue(product.IsDeleted);
//            }
//        }
//    }

//}
