using Microsoft.EntityFrameworkCore;
using WHMSData.Models;

namespace WHMSData.Context
{
    public interface IWHMSContext
    {
        DbSet<Address> Addresses { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Partner> Partners { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<ProductWarehouse> ProductWarehouse { get; set; }

        DbSet<Town> Towns { get; set; }

        DbSet<Transfer> Transfers { get; set; }

        DbSet<Unit> Units { get; set; }

        DbSet<Warehouse> Warehouses { get; set; }

        int SaveChanges();
    }
}
