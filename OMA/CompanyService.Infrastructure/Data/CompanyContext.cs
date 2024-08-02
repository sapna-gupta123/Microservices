using CompanyService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService.Infrastructure.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.NewGuid(), // Optionally use a static GUID for consistent seeding
                    Name = "ABC Corp",
                    StreetAddress = "123 Elm Street",
                    City = "Springfield",
                    State = "IL",
                    PostalAddress = "PO Box 456",
                    Zip = "62701",
                    ContactNumber = "555-1234"
                },
                new Company
                {
                    Id = Guid.NewGuid(), // Optionally use a static GUID for consistent seeding
                    Name = "XYZ Inc",
                    StreetAddress = "456 Oak Avenue",
                    City = "Metropolis",
                    State = "NY",
                    PostalAddress = "PO Box 789",
                    Zip = "10001",
                    ContactNumber = "555-5678"
                }
            );
        }
    }
}
