using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Infrastructure.Data
{
    public class RetailSystemDbContext : IdentityDbContext<User, Role, Guid>
    {
        public RetailSystemDbContext(DbContextOptions<RetailSystemDbContext> options): base(options)
        {
            
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantCart> ProductVariantCarts { get; set; }
        public DbSet<Size> Sizes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(RetailSystemDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
