using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class GetOrderById_Should
    {
        [TestMethod]
        public void Succeed()  //int orderId
        {
            Order order = new Order { Id = 1 };

            var dbName = ((nameof(GetOrderById_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Orders.Add(order);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var getOrder = sut.GetOrderById(1);
                Assert.AreEqual(order.Id, getOrder.Id);
            }
        }
        [TestMethod]
        public void ThrowException_WhenOrderDoesntExist()  //int orderId
        {
            Order order = new Order { Id = 1 };

            var dbName = ((nameof(GetOrderById_Should)) + (nameof(ThrowException_WhenOrderDoesntExist)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Orders.Add(order);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetOrderById(2));
                Assert.AreEqual($"Order with ID: {2} doesn't exist!", ex.Message);
            }
        }
        [TestMethod]
        public void ThrowException_WhenOrderIsDeleted()  //int orderId
        {
            var dbName = ((nameof(GetOrderById_Should)) + (nameof(ThrowException_WhenOrderIsDeleted)));
            var options = TestUtils.GetOptions(dbName);
            Order order = new Order { Id = 1, IsDeleted = true };
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Orders.Add(order);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetOrderById(1));
                Assert.AreEqual($"Order with ID: {1} doesn't exist!", ex.Message);
            }
        }
    }
}
