using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class AddProduct_Should
    {
        [TestMethod]
        public void Succeed()
        {
            //Arrange
            var dbName = ((nameof(AddProduct_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            Product product1 = new Product { Name = "Product1" };
            Product product2 = new Product { Name = "Product2" };
            using (var arrangeContext = new WHMSContext(options))
            {
                //arrangeContext.Partners.Add(partner);
                arrangeContext.Orders.Add(new Order { Id = 1,Products=new List<Product> { product1} });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(options))
            {
                var sut = new OrderService(assertContext);
                var addProduct = sut.AddProductToOrder(1, product2,1);
                Assert.IsTrue(addProduct.Products.Contains(product2));
            }
        }
    }
}
