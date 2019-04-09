using Microsoft.VisualStudio.TestTools.UnitTesting;
using WHMSData.Context;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class Edit
    {
        [TestMethod]
        public void Type_ShouldSucceed()  //int orderId, OrderType type
        {
            OrderType type = OrderType.Sell;

            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Type_ShouldSucceed))))
            {
                arrangeContext.Orders.Add(new Order { Id = 1, Type = OrderType.Buy });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Type_ShouldSucceed))))
            {
                var sut = new OrderService(assertContext);
                var editType = sut.EditType(1, type);
                Assert.AreEqual(type, editType.Type);
            }
        }

        [TestMethod]
        public void Partner_ShouldSucceed()  //int orderId, Partner newPartner
        {
            Partner partner = new Partner { Name = "Partner1" };
            Partner newPartner = new Partner { Name = "Partner2" };
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Partner_ShouldSucceed))))
            {
                arrangeContext.Orders.Add(new Order { Id = 1, Partner = partner });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Partner_ShouldSucceed))))
            {
                var sut = new OrderService(assertContext);
                var editPartner = sut.EditPartner(1, newPartner);
                Assert.AreEqual(newPartner, editPartner.Partner);
            }
        }

        [TestMethod]
        public void Comment_ShouldSucceed()  //int orderId, string comment
        {
            string comment = "some comments";
            using (var arrangeContext = new WHMSContext(TestUtils.GetOptions(nameof(Comment_ShouldSucceed))))
            {
                arrangeContext.Orders.Add(new Order { Id = 1 });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new WHMSContext(TestUtils.GetOptions(nameof(Comment_ShouldSucceed))))
            {
                var sut = new OrderService(assertContext);
                var editComment = sut.EditComment(1, comment);
                Assert.AreEqual(comment, editComment.Comment);
            }
        }
    }
}
