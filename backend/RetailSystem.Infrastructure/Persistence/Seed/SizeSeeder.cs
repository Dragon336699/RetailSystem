using RetailSystem.Domain.Entities;

namespace RetailSystem.Infrastructure.Persistence.Seed
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Security.Cryptography;
    using System.Text;

    namespace RetailSystem.Infrastructure.Persistence.Seed
    {
        public class SizeSeeder : IEntityTypeConfiguration<Size>
        {
            public void Configure(EntityTypeBuilder<Size> builder)
            {
                builder.HasKey(s => s.Id);

                builder.Property(s => s.SizeNumber).HasPrecision(3, 1);

                var sizes = new List<Size>();

                for (decimal val = 36.0m; val <= 43.0m; val += 0.5m)
                {
                    sizes.Add(new Size
                    {
                        Id = CreateGuidFromDecimal(val),
                        SizeNumber = val
                    });
                }

                builder.HasData(sizes);
            }

            private static Guid CreateGuidFromDecimal(decimal value)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes("Size_" + value.ToString("0.0")));
                    return new Guid(hash);
                }
            }
        }
    }
}
