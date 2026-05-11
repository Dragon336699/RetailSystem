using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsAsync(int skip, int take);
        Task<List<Product>> GetFilteredProduct(Guid categoryId, int skip = 0, int take = 10);
    }
}
