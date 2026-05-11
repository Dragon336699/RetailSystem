using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.UI.Services.Intefaces;

namespace RetailSystem.UI.Services.ApiClients
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpClient;
        public ProductApiClient(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient"); ;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("products");
        }

        public async Task<List<ProductDto>> GetFeatureProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("products?skip=0&take=4");
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"products/{id}");
        }
    }
}
