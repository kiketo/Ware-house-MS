using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WHMSData.Models;

namespace WHMSData.Context

{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductWarehouse> ProductWarehouse { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Order> Orders { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer($"Server={Constants.serverName};Database=WarehouseMS;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductWarehouse>()
                    .HasKey(p => new { p.ProductId, p.WarehouseId });
        }

    }
}
