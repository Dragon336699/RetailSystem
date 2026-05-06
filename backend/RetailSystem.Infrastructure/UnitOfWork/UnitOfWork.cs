using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Infrastructure.Data;

namespace RetailSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RetailSystemDbContext _context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }

        public UnitOfWork(
            RetailSystemDbContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository
            )
        {
            _context = context;
            Products = productRepository;
            Categories = categoryRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
