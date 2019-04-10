using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class GetOrdersByPartner_Should
    {
        [TestMethod]
        public void Succeed()  
        {
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };
            Partner partnerOther = new Partner { Name = "Other Partner" };

            var dbName = ((nameof(GetOrdersByPartner_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new WHMSContext(options))
            {
                arrangeContext.Orders.Add(new Order { Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Partner = partnerOther, Products = new List<Product> { product } });

                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(options))
            {
                var sut = new OrderService(assertContext);
                var getOrdersByPartner = sut.GetOrdersByPartner(partner);
                Assert.AreEqual(2, getOrdersByPartner.Count);
            }
        }
        [TestMethod]
        public void ThrowException_WhenSuchOrderDoesntExist()  
        {
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };
            Partner partnerOther = new Partner { Name = "Other Partner" };

            var dbName = ((nameof(GetOrdersByPartner_Should)) + (nameof(ThrowException_WhenSuchOrderDoesntExist)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new WHMSContext(options))
            {
                arrangeContext.Orders.Add(new Order { Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Partner = partner, Products = new List<Product> { product } });

                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(options))
            {
                var sut = new OrderService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetOrdersByPartner(partnerOther));
                Assert.AreEqual($"Orders of Partner: {partnerOther} doesn't exist!", ex.Message);
            }
        }
        [TestMethod]
        public void ThrowException_WhenOrderIsDeleted()  
        {
            Partner partner = new Partner { Name = "Partner", };
            Product product = new Product { Name = "Product" };
            Partner partnerOther = new Partner { Name = "Other Partner" };

            var dbName = ((nameof(GetOrdersByPartner_Should)) + (nameof(ThrowException_WhenOrderIsDeleted)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new WHMSContext(options))
            {
                arrangeContext.Orders.Add(new Order { Partner = partner, Products = new List<Product> { product }, IsDeleted=true });
                arrangeContext.Orders.Add(new Order { Partner = partnerOther, Products = new List<Product> { product } });

                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(options))
            {
                var sut = new OrderService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetOrdersByPartner(partner));
                Assert.AreEqual($"Orders of Partner: {partner} doesn't exist!", ex.Message);
            }
        }
    }
}
