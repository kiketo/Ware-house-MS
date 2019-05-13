using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WHMSData.Context;
using WHMSData.Models;

namespace WHMS.Services.Tests.ProductServiceTest
{
    [TestClass]
    public class DeleteProductAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_IfProductIsNull()
        {
            var dbName = ((nameof(DeleteProductAsync_Should)) + (nameof(ThrowException_IfProductIsNull)));
            var options = TestUtils.GetOptions(dbName);

            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                int id = 1;
                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.DeleteProductAsync(id));
                string expected = $"Product with ID: `{id}` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_IfProductIsAlreadyDeleted()
        {
            var dbName = ((nameof(DeleteProductAsync_Should)) + (nameof(ThrowException_IfProductIsAlreadyDeleted)));
            var options = TestUtils.GetOptions(dbName);
            int id = 1;
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(new Product() {Id= id, IsDeleted = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var ex =await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.DeleteProductAsync(id));
                string expected = $"Product with ID: `{id}` doesn't exist!";
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        public async Task MarkProductAsDeleted()
        {
            var dbName = ((nameof(DeleteProductAsync_Should)) + (nameof(MarkProductAsDeleted)));
            var options = TestUtils.GetOptions(dbName);
            int id = 1;

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Products.Add(new Product() { Id=id, IsDeleted = false });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ApplicationDbContext(options))
            {
                var sut = new ProductService(assertContext);
                var product = await sut.DeleteProductAsync(id);

                Assert.IsTrue(product.IsDeleted == true);
                Assert.IsNotNull(product);
            }
        }
    }
}
