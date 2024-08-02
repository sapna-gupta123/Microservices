using CategoryService.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CategoryService.Infrastructure.Data
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Mobile",
                    CategoryType = "Electronics"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "TV",
                    CategoryType= "HomeAppliances"
                }
            );
           
        }
    }
}
