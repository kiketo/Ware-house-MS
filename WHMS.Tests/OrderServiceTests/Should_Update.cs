using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.OrderServiceTests
{
    [TestClass]
    public class Should_Update
    {
        [TestMethod]
        public async Task Success()
        {
            var dbName = nameof(Success);

            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Orders.Add(new Order() { Partner = new Partner() { Name = "Goshka"}, Id = 66 });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var order = assertContext.Orders.FirstOrDefault(o => o.Id == 66);
                var mockPartner = new Partner();
                mockPartner.Name = "Gosheto2";
                order.Partner = mockPartner;
                var updatedOrder = await sut.UpdateAsync(order);

                Assert.AreEqual("Gosheto2", updatedOrder.Partner.Name);
                Assert.AreEqual(66, updatedOrder.Id);

            }
        }
    }
}
