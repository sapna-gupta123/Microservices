using DomainClass.User;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.SqlServer.Common
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Email = "admin@admin.com",
                    Name = "Admin",
                    Password = "admin", // Ensure this is hashed/encrypted in real scenarios
                    IsActive = true,
                    LastLogin = DateTime.Now,
                    Roles = "Admin"
                }
            );
        }
    }
}
