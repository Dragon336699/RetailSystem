using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RetailSystem.SharedLibrary.Dtos.Users;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task RegisterCustomerAsync(RegisterCustomerCommand command)
        {
            var user = new User
            {
                FullName = command.FullName,
                UserName = command.UserName,
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
                throw new Exception("Create user failed");

            await _userManager.AddToRoleAsync(user, "Customer");
        }

        public async Task<List<UserDto>> GetRegisteredCustomers()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            return _mapper.Map<List<UserDto>>(customers);
        }
    }
}
