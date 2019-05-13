using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

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
        public static ApplicationUser User()
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "tester";
            return user;
        }

    }
}
