using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.SharedLibrary.Dtos.Sizes;
using RetailSystem.UI.Services.Intefaces;

namespace RetailSystem.UI.Services.ApiClients
{
    public class SizeApiClient : ISizeApiClient
    {
        private readonly HttpClient _httpClient;
        public SizeApiClient(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient"); ;
        }

        public async Task<List<SizeDto>> GetSizesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SizeDto>>("sizes");
        }
    }
}
