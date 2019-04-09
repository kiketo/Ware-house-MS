using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;

namespace WHMS.Services.Tests.OrderServiceTest
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod] 
        public void Succeed()  //(OrderType type, Partner partner, Product product, int qty, string comment = null)
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName:"Succeed")
                .Options;
            var context = new WHMSContext(options);
            var sut = new OrderService(context);
        }
    }
}
