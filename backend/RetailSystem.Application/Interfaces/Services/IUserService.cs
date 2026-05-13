using RetailSystem.SharedLibrary.Dtos.Users;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetRegisteredCustomers();
        Task RegisterCustomerAsync(RegisterCustomerCommand command);
        Task<string> Login(LoginCommand command);
        Task<UserDto> GetUserInfo(string userId);
    }
}
