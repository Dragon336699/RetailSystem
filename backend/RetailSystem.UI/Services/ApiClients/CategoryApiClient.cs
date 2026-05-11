using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.UI.Services.Intefaces;

namespace RetailSystem.UI.Services.ApiClients
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly HttpClient _httpClient;
        public CategoryApiClient(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>("categories");
        }
    }
}
