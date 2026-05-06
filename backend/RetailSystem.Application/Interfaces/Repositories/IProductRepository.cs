using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsAsync(int skip, int take);
    }
}
