using Microsoft.EntityFrameworkCore;
using WHMSData.Models;
using WHMSData.Utills;

namespace WHMSData.Context
{
    public class WHMSContext : DbContext, IWHMSContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductWarehouse> ProductWarehouse { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Transfer> Transfers { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Server={Constants.serverName};Database=WarehouseMS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductWarehouse>()
                    .HasKey(p => new { p.ProductId, p.WarehouseId });


            //modelBuilder.Entity<Address>()
            //    .HasKey(k => k.Id);




            //modelBuilder.Entity<Town>()
            //    .HasKey(t => t.Id);




            //modelBuilder.Entity<Order>()
            //    .HasKey(k => k.Id);




            //modelBuilder.Entity<Product>()
            //    .HasKey(k => k.Id);




            //modelBuilder.Entity<Transfer>()
            //    .HasKey(k => k.Id);



            //modelBuilder.Entity<Unit>()
            //    .HasKey(k => k.Id);


            //modelBuilder.Entity<Warehouse>()
            //    .HasKey(k => k.Id);
        }
    }
}
