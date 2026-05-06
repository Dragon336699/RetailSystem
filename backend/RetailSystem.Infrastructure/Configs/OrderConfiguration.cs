using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Infrastructure.Configs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade); //Xem lại sau

            builder.HasMany(o => o.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.TotalAmount)
                .HasPrecision(18, 2);
        }
    }
}
