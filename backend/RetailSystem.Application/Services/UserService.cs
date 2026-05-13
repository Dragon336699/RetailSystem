using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Domain.Entities;
using RetailSystem.SharedLibrary.Dtos.Users;
using RetailSystem.SharedLibrary.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetailSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(UserManager<User> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
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

        public async Task<string> Login(LoginCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.UserName);

            if (user == null)
            {
                throw new UnauthorizeException("Invalid username or password");
            }

            if (!await _userManager.CheckPasswordAsync(user, command.Password))
            {
                throw new UnauthorizeException("Invalid username or password");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtValue = GenerateJwt(authClaims);
            var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtValue);

            return jwtString;
        }

        public async Task<UserDto> GetUserInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        private JwtSecurityToken GenerateJwt(List<Claim> authClaims)
        {
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            return new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.UtcNow.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
