using RetailSystem.SharedLibrary.Dtos.Users;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetRegisteredCustomers();
    }
}
