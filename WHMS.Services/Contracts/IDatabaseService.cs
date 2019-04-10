using System.Collections.Generic;
using WHMSData.Models;

namespace WHMS.Services
{
    public interface IDatabaseService
    {
        List<Address> GetAddresses();

        List<Category> GetCategories();

        List<Order> GetOrders();

        List<Partner> GetPartners();

        List<Product> GetProducts();

        List<Town> GetTowns();

        List<Unit> GetUnits();

        List<Warehouse> GetWarehouses();

        void PushAddresses(List<Address> addresses);
    }
}