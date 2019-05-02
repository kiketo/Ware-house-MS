using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public async Task Succeed()  //(OrderType type, Partner partner, Product product, int qty, string comment = null)
        {
            //Arrange
            OrderType type = OrderType.Sell;
            Partner partner = new Partner { Name = "Partner1" };
            Product product = new Product { Name = "Product1" };

            //Act&Assert
            using (var assertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Add_Should) +nameof(Succeed))))
            {
            //    var sut = new OrderService(assertContext);
            //    var addOrder = await sut.AddAsync(type, partner, product, 5);

            //    Assert.AreEqual(type, addOrder.Type);
            //    Assert.AreEqual(partner, addOrder.Partner);
               // Assert.AreEqual(product, addOrder.Products.FirstOrDefault());
               //TODO
            }
        }
    }
}
