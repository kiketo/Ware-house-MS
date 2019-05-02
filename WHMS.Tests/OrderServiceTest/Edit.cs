using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class Edit
    {
        [TestMethod]
        public async Task Type_ShouldSucceed()  //int orderId, OrderType type
        {
            OrderType type = OrderType.Sell;

            var dbName = ((nameof(Edit)) + (nameof(Type_ShouldSucceed)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Orders.Add(new Order { Id = 1, Type = OrderType.Buy });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var editType = await sut.EditTypeAsync(1, type);
                Assert.AreEqual(type, editType.Type);
            }
        }

        [TestMethod]
        public async Task Partner_ShouldSucceed()  //int orderId, Partner newPartner
        {
            Partner partner = new Partner { Name = "Partner1" };
            Partner newPartner = new Partner { Name = "Partner2" };

            var dbName = ((nameof(Edit)) + (nameof(Partner_ShouldSucceed)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Orders.Add(new Order { Id = 1, Partner = partner });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var editPartner =await sut.EditPartnerAsync(1, newPartner);
                Assert.AreEqual(newPartner, editPartner.Partner);
            }
        }

        [TestMethod]
        public async Task Comment_ShouldSucceed()  //int orderId, string comment
        {
            string comment = "some comments";

            var dbName = ((nameof(Edit)) + (nameof(Comment_ShouldSucceed)));
            var options = TestUtils.GetOptions(dbName);
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Orders.Add(new Order { Id = 1 });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new OrderService(assertContext);
                var editComment = await sut.EditCommentAsync(1, comment);
                Assert.AreEqual(comment, editComment.Comment);
            }
        }
    }
}
