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
                .Include(p => p.ProductImages)
                .Include(p => p.Categories)
                .Include(p => p.ProductVariants)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
