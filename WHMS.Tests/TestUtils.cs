using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
