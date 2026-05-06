using RetailSystem.Application.Dtos.Products;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync(int skip = 0, int take = 10);
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<ProductDto> AddProductAsync(CreateProductCommand product);
    }
}
