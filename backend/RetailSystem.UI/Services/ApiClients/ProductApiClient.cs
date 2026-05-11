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

        public async Task<List<ProductDto>> GetFeatureProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("products");
        }
    }
}
