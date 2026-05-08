using RetailSystem.Application.Dtos.Users;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetRegisteredCustomers();
    }
}
