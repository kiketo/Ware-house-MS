using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class GetOrdersByType_Should
    {
        [TestMethod]
        public void Succeed()  //(OrderType type, DateTime fromDate, DateTime toDate)
        {
            string outPeriod = "30/01/2000";
            string fromDate = "01/01/2018";
            string inPeriod = "01/02/2019";
            string toDate = "01/03/2020";
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrdersByType_Should)) + (nameof(Succeed)))))
            {
                arrangeContext.Orders.Add(new Order { Id = 1, Type = OrderType.Sell, CreatedOn = DateTime.Parse(inPeriod), Partner=partner, Products=new List<Product> { product} });
                arrangeContext.Orders.Add(new Order { Id = 2, Type = OrderType.Sell, CreatedOn = DateTime.Parse(inPeriod), Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Id = 3, Type = OrderType.Sell, CreatedOn = DateTime.Parse(outPeriod), Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Id = 4, Type = OrderType.Buy, CreatedOn = DateTime.Parse(inPeriod), Partner = partner, Products = new List<Product> { product } });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrdersByType_Should)) + (nameof(Succeed)))))
            {
                var sut = new OrderService(assertContext);
                var getOrdersByType = sut.GetOrdersByType(OrderType.Sell, DateTime.Parse(fromDate), DateTime.Parse(toDate));
                Assert.AreEqual(2,getOrdersByType.Count);
            }
        }
        [TestMethod]
        public void ThrowException_WhenSuchOrderDoesntExist()  //(OrderType type, DateTime fromDate, DateTime toDate)
        {
            OrderType type = OrderType.Sell;
            DateTime outPeriod = DateTime.Parse("30/01/2000");
            DateTime fromDate = DateTime.Parse("01/01/2018");
            //string inPeriod = "01/02/2019";
            DateTime toDate = DateTime.Parse("01/03/2020");
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrdersByType_Should)) + (nameof(ThrowException_WhenSuchOrderDoesntExist)))))
            {
                arrangeContext.Orders.Add(new Order { Id = 1, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Id = 2, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Id = 3, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Id = 4, Type = OrderType.Buy, CreatedOn =outPeriod, Partner = partner, Products = new List<Product> { product } });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrdersByType_Should)) + (nameof(ThrowException_WhenSuchOrderDoesntExist)))))
            {
                var sut = new OrderService(assertContext);
                var ex =Assert.ThrowsException<ArgumentException>(()=> sut.GetOrdersByType(OrderType.Sell, fromDate, toDate));
                Assert.AreEqual($"Order with Type: {type} from date {fromDate} to date {toDate} doesn't exist!", ex.Message);
            }
        }
        [TestMethod]
        public void ThrowException_WhenOrderIsDeleted()  //int orderId
        {
            OrderType type = OrderType.Sell;
            DateTime outPeriod = DateTime.Parse("30/01/2000");
            DateTime fromDate = DateTime.Parse("01/01/2018");
            DateTime inPeriod = DateTime.Parse("01/02/2019");
            DateTime toDate = DateTime.Parse("01/03/2020");
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrdersByType_Should)) + (nameof(ThrowException_WhenOrderIsDeleted)))))
            {
                arrangeContext.Orders.Add(new Order { Id = 1, Type = type, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product }, IsDeleted=true });
                arrangeContext.Orders.Add(new Order { Id = 2, Type = type, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product }, IsDeleted=true });
                arrangeContext.Orders.Add(new Order { Id = 3, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
                arrangeContext.Orders.Add(new Order { Id = 4, Type = OrderType.Buy, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product } });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(TestUtils.GetOptions((nameof(GetOrdersByType_Should)) + (nameof(ThrowException_WhenOrderIsDeleted)))))
            {
                var sut = new OrderService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetOrdersByType(OrderType.Sell, fromDate, toDate));
                Assert.AreEqual($"Order with Type: {type} from date {fromDate} to date {toDate} doesn't exist!", ex.Message);
            }
        }
    }
}
