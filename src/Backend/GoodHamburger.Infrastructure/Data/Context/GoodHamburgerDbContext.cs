using GoodHamburger.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data.Context
{
    public class GoodHamburgerDbContext(DbContextOptions<GoodHamburgerDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
