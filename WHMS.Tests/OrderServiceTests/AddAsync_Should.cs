using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Tests.OrderServiceTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public async Task Succeed()  //(OrderType type, Partner partner, IDictionary<ProductWarehouse, int> pws, ApplicationUser user, string comment = null)
        {
            var dbName = nameof(Succeed);

            var options = TestUtils.GetOptions(dbName);
            using (var assertContext = new ApplicationDbContext(options))
            {
                var service = new OrderService(assertContext);
                var sut = await service.AddAsync(OrderType.Sell, new Partner() { Name = "Pesho"}, new Dictionary<ProductWarehouse, int>(), new ApplicationUser(), "mockComment");

                Assert.AreEqual("Pesho", sut.Partner.Name);
                Assert.IsNotNull(sut);

            }
        }
        
    }
}
