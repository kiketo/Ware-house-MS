//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace WHMS.Services.Tests.OrderServiceTests
//{
//    [TestClass]
//    public class GetOrder_Should
//    {
//        [TestMethod]

//        //public void Get_Order_By_Id_Should()
//        //{
//        //    var dbName = nameof(Should_Throw_Exception_If_Product_Deleted);

//        //    var options = TestUtils.GetOptions(dbName);
//        //    using (var arrangeContext = new ApplicationDbContext(options))
//        //    {
//        //        arrangeContext.Products.Add(new Product() { Name = "productName", IsDeleted = true });
//        //        arrangeContext.SaveChanges();
//        //    }
//        //    using (var assertContext = new ApplicationDbContext(options))
//        //    {
//        //        var sut = new ProductService(assertContext);
//        //        var ex = Assert.ThrowsException<ArgumentException>(() => sut.FindByNameAsync("productName"));
//        //        string expected = "Product `productName` doesn't exist!";
//        //        Assert.AreEqual(expected, ex.Message);
//        //    }
//        //}

    
//}
////public async Task<Order> GetOrderByIdAsync(int orderId)
////{
////    Order orderToShow = await this.context.Orders
////        .Where(o => o.Id == orderId && !o.IsDeleted)
////        .Include(o => o.OrderProductsWarehouses)
////        .Include(o => o.Partner)
////        .Include(o => o.Creator)
////        .FirstOrDefaultAsync();
////    if (orderToShow == null || orderToShow.IsDeleted)
////    {
////        throw new ArgumentException($"Order with ID: {orderId} doesn't exist!");
////    }
////    return orderToShow;
////}

////public async Task<ICollection<Order>> GetOrdersByTypeAsync(OrderType type, DateTime fromDate, DateTime toDate)
////{
////    List<Order> ordersToShow = await this.context.Orders
////        .Where(o => o.Type == type && !o.IsDeleted)
////        .Where(o => o.CreatedOn >= fromDate)
////        .Where(o => o.CreatedOn <= toDate)
////        .Include(p => p.Partner)
////        .Include(p => p.OrderProductsWarehouses)
////        .Include(o => o.Creator)
////        .ToListAsync();

////    if (ordersToShow.Count == 0)
////    {
////        throw new ArgumentException($"Order with Type: {type} from date {fromDate} to date {toDate} doesn't exist!");
////    }
////    return ordersToShow;
////}

////public async Task<ICollection<Order>> GetOrdersByPartnerAsync(Partner partner)
////{
////    List<Order> ordersToShow = await this.context.Orders
////        .Where(o => o.Partner == partner && !o.IsDeleted)
////        .Include(p => p.Partner)
////        .Include(p => p.OrderProductsWarehouses)
////        .Include(o => o.Creator)
////        .ToListAsync();

////    if (ordersToShow.Count == 0)
////    {
////        throw new ArgumentException($"Orders of Partner: {partner} doesn't exist!");
////    }
////    return ordersToShow;
////}

////public async Task<Order> DeleteOrderAsync(int id)
////{
////    Order orderToDelete = await this.GetOrderByIdAsync(id);
////    orderToDelete.IsDeleted = true;
////    orderToDelete.ModifiedOn = DateTime.Now;

////    await this.context.SaveChangesAsync();

////    return orderToDelete;
////}