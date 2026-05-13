using Azure;
using RetailSystem.SharedLibrary.Contracts.Users;
using RetailSystem.SharedLibrary.Dtos.Users;
using RetailSystem.UI.Services.Intefaces;

namespace RetailSystem.UI.Services.ApiClients
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _httpClient;
        public UserApiClient(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public async Task RegisterUser(RegisterCustomerRequest request)
        {
             var response = await _httpClient.PostAsJsonAsync("users/register", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
    }
}
