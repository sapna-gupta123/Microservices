using Microsoft.EntityFrameworkCore;
using ProductService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
        .Property(p => p.ProductImage)
        .HasColumnType("varbinary(max)"); // Use varbinary(max) for SQL Server

            // Seed initial data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.NewGuid(), // Optionally use a static GUID for consistent seeding
                    Name = "TV",
                    CategoryId = new Guid("B2532BD0-997B-4A65-B507-A1E32C6DC598"),
                    CategoryName = "Electronics",
                    Manufacturer = "LG",
                    Quantity = 10,
                    Price = 500.0m,
                    ProductImage = Array.Empty<byte>()

                }
                // Add more seeded products as needed
            ) ;
        }
    }

}
