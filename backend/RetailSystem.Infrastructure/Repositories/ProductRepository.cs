using Microsoft.EntityFrameworkCore;
using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Domain.Entities;
using RetailSystem.Infrastructure.Data;
using System.Linq;

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

        public async Task<List<Product>> GetFilteredProduct(Guid categoryId, int skip = 0, int take = 10)
        {
            return await _context.Products
                            .OrderBy(p => p.CreatedAt)
                            .Skip(skip)
                            .Take(take)
                            .Where(p => p.Categories.Any(c => c.Id == categoryId))
                            .AsSplitQuery()
                            .Include(p => p.ProductImages)
                            .Include(p => p.Categories)
                            .Include(p => p.ProductVariants)
                            .ToListAsync();
        }
    }
}
