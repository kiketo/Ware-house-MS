using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class SetMargin_Should
    {
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Not_Found_Null_SM()
        {

            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Not_Found_Null_SM))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.SetMargin(1, 5.0000212));
                string expected = "Product does not exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        public void Should_Throw_Exception_If_Product_Is_Deleted_SM()
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Is_Deleted_SM))))
            {
                arrangeContext.Products.Add(new Product() { IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Throw_Exception_If_Product_Is_Deleted_SM))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.SetBuyPrice(1, 43.432m));
                string expected = "Product does not exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        [DataRow(-40.0424322)]
        [DataRow(-324.65464521)]
        [DataRow(-3)]
        [DataRow(-0.21)]
        public void Throw_Exception_If_Product_Margin_Is_Less_Than_Zero_SM(double number)
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Throw_Exception_If_Product_Margin_Is_Less_Than_Zero_SM))))
            {
                arrangeContext.Products.Add(new Product() { Name = "Name", MarginInPercent = 0 });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Throw_Exception_If_Product_Margin_Is_Less_Than_Zero_SM))))
            {
                var sut = new ProductService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => (sut.SetMargin(1, number)));
                var expectedMessage = $"The price margin cannot be negative number";
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }
        [TestMethod]
        [DataRow(40.0424322)]
        [DataRow(324.65464521)]
        [DataRow(3)]
        [DataRow(0.21)]
        public void Should_Set_Buy_Price_(double number)
        {
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Set_Buy_Price_))))
            {
                arrangeContext.Products.Add(new Product() { Name = "Name", MarginInPercent = 0 });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Should_Set_Buy_Price_))))
            {
                var sut = new ProductService(assertContext);
                var product = sut.SetMargin(1, number);

                Assert.AreEqual(number, product.MarginInPercent);
                Assert.IsNotNull(product);
                Assert.IsInstanceOfType(product, typeof(Product));
            }
        }
    }
}
