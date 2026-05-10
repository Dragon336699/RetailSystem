using Microsoft.EntityFrameworkCore;
using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Domain.Entities;
using RetailSystem.Infrastructure.Data;

namespace RetailSystem.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(RetailSystemDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsAsync(int skip = 0, int take = 10)
        {
            return await _context.Products
                            .OrderBy(p => p.CreatedAt)
                            .Skip(skip)
                            .Take(take)
                            .AsSplitQuery()
                            .Include(p => p.ProductImages)
                            .Include(p => p.Categories)
                            .Include(p => p.ProductVariants)
                            .ToListAsync();
        }
    }
}
