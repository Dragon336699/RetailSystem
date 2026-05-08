using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Domain.Entities;
using RetailSystem.Infrastructure.Data;

namespace RetailSystem.Infrastructure.Repositories
{
    public class ProductVariantRepository : GenericRepository<ProductVariant>, IProductVariantRepository
    {
        public ProductVariantRepository(RetailSystemDbContext context) : base(context)
        {
        }
    }
}
