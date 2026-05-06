using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Infrastructure.Persistence.Seed
{
    public class ColorSeeder : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasData(
                new Color { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), ColorName = "Black", ColorCode = "#000000" },
                new Color { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), ColorName = "White", ColorCode = "#FFFFFF" },
                new Color { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), ColorName = "Grey", ColorCode = "#808080" },
                new Color { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), ColorName = "Red", ColorCode = "#FF0000" },
                new Color { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), ColorName = "Navy Blue", ColorCode = "#000080" },
                new Color { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), ColorName = "Green", ColorCode = "#008000" },
                new Color { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), ColorName = "Yellow", ColorCode = "#FFFF00" },
                new Color { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), ColorName = "Orange", ColorCode = "#FFA500" },
                new Color { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), ColorName = "Brown", ColorCode = "#A52A2A" },
                new Color { Id = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), ColorName = "Pink", ColorCode = "#FFC0CB" },
                new Color { Id = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), ColorName = "Purple", ColorCode = "#800080" },
                new Color { Id = Guid.Parse("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), ColorName = "Beige", ColorCode = "#F5F5DC" },
                new Color { Id = Guid.Parse("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), ColorName = "Cream", ColorCode = "#FFFDD0" },
                new Color { Id = Guid.Parse("EEEEEEEE-EEEE-EEEE-EEEE-EEEEEEEEEEEE"), ColorName = "Silver", ColorCode = "#C0C0C0" },
                new Color { Id = Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), ColorName = "Gold", ColorCode = "#D4AF37" }
            );
        }
    }
}
