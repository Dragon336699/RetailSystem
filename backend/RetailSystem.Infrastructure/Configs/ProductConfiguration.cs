using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Infrastructure.Configs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(p => p.ProductVariants)
                    .WithOne(v => v.Product)
                    .HasForeignKey(v => v.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Categories)
                    .WithMany(ca => ca.Products)
                    .UsingEntity(j => j.ToTable("ProductCategory"));

            builder.HasMany(p => p.ProductImages)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Property(p => p.RatingAverage)
                .HasPrecision(2, 1);
        }
    }
}
