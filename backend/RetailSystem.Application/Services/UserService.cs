using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RetailSystem.SharedLibrary.Dtos.Users;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetRegisteredCustomers()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            return _mapper.Map<List<UserDto>>(customers);
        }
    }
}
