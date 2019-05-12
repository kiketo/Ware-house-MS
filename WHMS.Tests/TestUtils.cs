using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Context;

namespace WHMS.Services.Tests
{
    public static class TestUtils
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions (string databaseName)
        {
            var provider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(provider)
                .Options;
        }

        public static async Task ThrowsAsync<TException>(Func<Task> action, bool allowDerivedTypes = true)
        {
            try
            {
                await action();
                Assert.Fail("Delegate did not throw expected exception " + typeof(TException).Name + ".");
            }
            catch (Exception ex)
            {
                if (allowDerivedTypes && !(ex is TException))
                    Assert.Fail("Delegate threw exception of type " + ex.GetType().Name + ", but " + typeof(TException).Name + " or a derived type was expected.");
                if (!allowDerivedTypes && ex.GetType() != typeof(TException))
                    Assert.Fail("Delegate threw exception of type " + ex.GetType().Name + ", but " + typeof(TException).Name + " was expected.");
            }
        }
    }
}
