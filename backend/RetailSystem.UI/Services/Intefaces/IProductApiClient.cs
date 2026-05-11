using RetailSystem.SharedLibrary.Dtos.Products;

namespace RetailSystem.UI.Services.Intefaces
{
    public interface IProductApiClient
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<List<ProductDto>> GetFeatureProducts();
        Task<ProductDto> GetProductByIdAsync(Guid id);
    }
}
