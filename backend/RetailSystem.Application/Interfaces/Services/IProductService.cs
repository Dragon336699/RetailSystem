using RetailSystem.SharedLibrary.Dtos.Products;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetFeatureProductsAsync();
        Task<List<ProductDto>> GetProductsAsync(int skip = 0, int take = 10);
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<ProductDto> AddProductAsync(CreateProductCommand product);
        Task<ProductDto> UpdateProductAsync(UpdateProductCommand product);
        Task DeleteProductAsync(Guid id);
    }
}
