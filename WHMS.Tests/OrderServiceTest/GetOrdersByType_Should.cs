using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class GetOrdersByType_Should //TODO
    {
        private IFormatProvider provider;
        [TestMethod]
        public async Task Succeed()  //(OrderType type, DateTime fromDate, DateTime toDate)
        {
            DateTime outPeriod = DateTime.ParseExact("30/01/2000", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime fromDate = DateTime.ParseExact("01/01/2018", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime inPeriod = DateTime.ParseExact("01/02/2019", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact("01/03/2020", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };

            var dbName = ((nameof(GetOrdersByType_Should)) + (nameof(Succeed)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                //arrangeContext.Orders.Add(new Order { Id = 1, Type = OrderType.Sell, CreatedOn =inPeriod, Partner=partner, Products=new List<Product> { product} });
                //arrangeContext.Orders.Add(new Order { Id = 2, Type = OrderType.Sell, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product } });
                //arrangeContext.Orders.Add(new Order { Id = 3, Type = OrderType.Sell, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
                //arrangeContext.Orders.Add(new Order { Id = 4, Type = OrderType.Buy, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product } });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var getOrdersByType = await sut.GetOrdersByTypeAsync(OrderType.Sell, fromDate, toDate);
                Assert.AreEqual(2,getOrdersByType.Count);
            }
        }
        [TestMethod]
        public void ThrowException_WhenSuchOrderDoesntExist()  //(OrderType type, DateTime fromDate, DateTime toDate)
        {
            
            OrderType type = OrderType.Sell;
            DateTime outPeriod = DateTime.ParseExact("30/01/2000","dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime fromDate = DateTime.ParseExact("01/01/2018", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact("01/03/2020", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };

            var dbName = ((nameof(GetOrdersByType_Should)) + (nameof(ThrowException_WhenSuchOrderDoesntExist)));
            var options = TestUtils.GetOptions(dbName);
            //using (var arrangeContext = new ApplicationDbContext(options))
            //{
            //    arrangeContext.Orders.Add(new Order { Id = 1, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
            //    arrangeContext.Orders.Add(new Order { Id = 2, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
            //    arrangeContext.Orders.Add(new Order { Id = 3, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
            //    arrangeContext.Orders.Add(new Order { Id = 4, Type = OrderType.Buy, CreatedOn =outPeriod, Partner = partner, Products = new List<Product> { product } });
            //    arrangeContext.SaveChanges();
            //}

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var ex =Assert.ThrowsException<ArgumentException>(async()=> await sut.GetOrdersByTypeAsync(OrderType.Sell, fromDate, toDate));
                Assert.AreEqual($"Order with Type: {type} from date {fromDate} to date {toDate} doesn't exist!", ex.Message);
            }
        }
        [TestMethod]
        public void ThrowException_WhenOrderIsDeleted()  //int orderId
        {
            OrderType type = OrderType.Sell;
            DateTime outPeriod = DateTime.ParseExact("30/01/2000", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime fromDate = DateTime.ParseExact("01/01/2018", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime inPeriod = DateTime.ParseExact("01/02/2019", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact("01/03/2020", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            Partner partner = new Partner { Name = "Partner" };
            Product product = new Product { Name = "Product" };

            var dbName = ((nameof(GetOrdersByType_Should)) + (nameof(ThrowException_WhenOrderIsDeleted)));
            var options = TestUtils.GetOptions(dbName);
            //using (var arrangeContext = new ApplicationDbContext(options))
            //{
            //    arrangeContext.Orders.Add(new Order { Id = 1, Type = type, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product }, IsDeleted=true });
            //    arrangeContext.Orders.Add(new Order { Id = 2, Type = type, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product }, IsDeleted=true });
            //    arrangeContext.Orders.Add(new Order { Id = 3, Type = type, CreatedOn = outPeriod, Partner = partner, Products = new List<Product> { product } });
            //    arrangeContext.Orders.Add(new Order { Id = 4, Type = OrderType.Buy, CreatedOn = inPeriod, Partner = partner, Products = new List<Product> { product } });
            //    arrangeContext.SaveChanges();
            //}

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(async() => await sut.GetOrdersByTypeAsync(OrderType.Sell, fromDate, toDate));
                Assert.AreEqual($"Order with Type: {type} from date {fromDate} to date {toDate} doesn't exist!", ex.Message);
            }
        }
    }
}
