//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace WHMS.Services.Tests.OrderServiceTest
//{
//    [TestClass]
//    public class GetOrdersByType_Should
//    {
//        [TestMethod]
//        public void Succeed()  //int orderId
//        {
//            Order order = new Order { Id = 1 };
//            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrderById_Should)) + (nameof(Succeed)))))
//            {
//                arrangeContext.Orders.Add(order);
//                arrangeContext.SaveChanges();
//            }

//            using (var assertContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrderById_Should)) + (nameof(Succeed)))))
//            {
//                var sut = new OrderService(assertContext);
//                var getOrder = sut.GetOrderById(1);
//                Assert.AreEqual(order.Id, getOrder.Id);
//            }
//        }
//        [TestMethod]
//        public void ThrowException_WhenOrderDoesntExist()  //int orderId
//        {
//            Order order = new Order { Id = 1 };
//            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrderById_Should)) + (nameof(Succeed)))))
//            {
//                arrangeContext.Orders.Add(order);
//                arrangeContext.SaveChanges();
//            }

//            using (var assertContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrderById_Should)) + (nameof(Succeed)))))
//            {
//                var sut = new OrderService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetOrderById(2));
//                Assert.AreEqual($"Order with ID: {2} doesn't exist!", ex.Message);
//            }
//        }
//        [TestMethod]
//        public void ThrowException_WhenOrderIsDeleted()  //int orderId
//        {
//            Order order = new Order { Id = 1, IsDeleted = true };
//            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrderById_Should)) + (nameof(Succeed)))))
//            {
//                arrangeContext.Orders.Add(order);
//                arrangeContext.SaveChanges();
//            }

//            using (var assertContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrderById_Should)) + (nameof(Succeed)))))
//            {
//                var sut = new OrderService(assertContext);
//                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetOrderById(1));
//                Assert.AreEqual($"Order with ID: {1} doesn't exist!", ex.Message);
//            }
//        }
//    }
//}
