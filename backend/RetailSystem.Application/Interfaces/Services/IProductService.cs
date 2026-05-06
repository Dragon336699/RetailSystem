using RetailSystem.Application.Dtos.Products;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(int skip = 0, int take = 10);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task AddProductAsync(CreateProductCommand product);
    }
}
