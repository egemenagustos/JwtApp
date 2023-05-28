using JwtApp.Back.Core.Domain;
using JwtApp.Back.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JwtApp.Back.Persistance.Contexts
{
    public class Context : DbContext
    {
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<AppRole>? AppRoles { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        public Context(DbContextOptions<Context> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
