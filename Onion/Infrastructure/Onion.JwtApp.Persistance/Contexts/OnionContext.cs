using Microsoft.EntityFrameworkCore;
using Onion.JwtApp.Domain.Entities;
using Onion.JwtApp.Persistance.Configurations;

namespace Onion.JwtApp.Persistance.Contexts
{
    public class OnionContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }

        public OnionContext(DbContextOptions<OnionContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        }
    }
}
