using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Infrastructure.Configs
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasMany(c => c.ProductVariantCarts)
                   .WithOne(pvc => pvc.Cart)
                   .HasForeignKey(pvc => pvc.CartId);

            builder.HasOne(c => c.User)
                   .WithOne(u => u.Cart)
                   .HasForeignKey<Cart>(c => c.UserId);
        }
    }
}
