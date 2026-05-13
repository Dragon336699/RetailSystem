using RetailSystem.SharedLibrary.Contracts.Users;
using RetailSystem.SharedLibrary.Dtos.Users;

namespace RetailSystem.UI.Services.Intefaces
{
    public interface IUserApiClient
    {
        Task RegisterUser(RegisterCustomerRequest request);
    }
}
