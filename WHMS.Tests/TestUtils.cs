using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WHMS.Services.Tests
{
    public static class TestUtils
    {
        public static DbContextOptions GetOptions (string databaseName)
        {
            return new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
