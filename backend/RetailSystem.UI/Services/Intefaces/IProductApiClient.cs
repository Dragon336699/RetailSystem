using RetailSystem.SharedLibrary.Dtos.Products;

namespace RetailSystem.UI.Services.Intefaces
{
    public interface IProductApiClient
    {
        Task<List<ProductDto>> GetFeatureProducts();
    }
}
