using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using RetailSystem.Application.Services;
using RetailSystem.Domain.Entities;
using RetailSystem.SharedLibrary.Dtos.Users;
using RetailSystem.SharedLibrary.Exceptions;

namespace RetailSystem.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _service;

        public UserServiceTest()
        {
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _mockMapper = new Mock<IMapper>();

            var inMemoryConfig = new Dictionary<string, string?>
            {
                { "JWT:Key", "supersecretkey1234567890123123123123123123" },
                { "JWT:Issuer", "issuer" },
                { "JWT:Audience", "audience" }
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemoryConfig).Build();

            _service = new UserService(_mockUserManager.Object, _mockMapper.Object, configuration);
        }

        [Fact]
        public async Task GetUserInfo_ShouldReturnMappedDto_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserName = "user1", FullName = "User One" };
            var dto = new UserDto { Id = userId, UserName = "user1", FullName = "User One" };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(dto);

            var result = await _service.GetUserInfo(userId.ToString());

            Assert.Equal(dto.Id, result.Id);
            Assert.Equal("User One", result.FullName);
            _mockUserManager.Verify(um => um.FindByIdAsync(userId.ToString()), Times.Once);
            _mockMapper.Verify(m => m.Map<UserDto>(user), Times.Once);
        }

        [Fact]
        public async Task GetUserInfo_ShouldThrowKeyNotFound_WhenUserNotFound()
        {
            var userId = Guid.NewGuid().ToString();
            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetUserInfo(userId));
        }

        [Fact]
        public async Task Login_ShouldReturnJwt_WhenCredentialsValid()
        {
            var cmd = new LoginCommand { UserName = "john", Password = "Pass1!" };
            var user = new User { Id = Guid.NewGuid(), UserName = "john", FullName = "John" };

            _mockUserManager.Setup(um => um.FindByNameAsync(cmd.UserName)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, cmd.Password)).ReturnsAsync(true);
            _mockUserManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Customer" });

            var inMemoryConfig = new Dictionary<string, string?>
            {
                { "JWT:Key", "supersecretkey1234567890123123123123123123" },
                { "JWT:Issuer", "issuer" },
                { "JWT:Audience", "audience" }
            };
            var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().AddInMemoryCollection(inMemoryConfig).Build();

            var serviceWithConfig = new UserService(_mockUserManager.Object, _mockMapper.Object, configuration);

            var jwt = await serviceWithConfig.Login(cmd);

            Assert.False(string.IsNullOrEmpty(jwt));
        }

        [Fact]
        public async Task Login_ShouldThrowUnauthorized_WhenUserNotFoundOrWrongPassword()
        {
            var cmd = new LoginCommand { UserName = "noone", Password = "bad" };

            _mockUserManager.Setup(um => um.FindByNameAsync(cmd.UserName)).ReturnsAsync((User?)null);

            var inMemoryConfig = new Dictionary<string, string?>
            {
                { "JWT:Key", "k" },
                { "JWT:Issuer", "i" },
                { "JWT:Audience", "a" }
            };
            var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().AddInMemoryCollection(inMemoryConfig).Build();

            var serviceWithConfig = new UserService(_mockUserManager.Object, _mockMapper.Object, configuration);

            await Assert.ThrowsAsync<UnauthorizeException>(() => serviceWithConfig.Login(cmd));
        }

        [Fact]
        public async Task RegisterCustomerAsync_ShouldCreateUserAndAddRole_WhenSuccess()
        {
            var cmd = new RegisterCustomerCommand { FullName = "John", UserName = "john", Password = "Pass1!" };

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), cmd.Password)).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), "Customer")).ReturnsAsync(IdentityResult.Success);

            await _service.RegisterCustomerAsync(cmd);

            _mockUserManager.Verify(um => um.CreateAsync(It.Is<User>(u => u.UserName == cmd.UserName && u.FullName == cmd.FullName), cmd.Password), Times.Once);
            _mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), "Customer"), Times.Once);
        }

        [Fact]
        public async Task RegisterCustomerAsync_ShouldThrow_WhenCreateFails()
        {
            var cmd = new RegisterCustomerCommand { FullName = "John", UserName = "john", Password = "Pass1!" };

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), cmd.Password)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            await Assert.ThrowsAsync<Exception>(() => _service.RegisterCustomerAsync(cmd));
        }

        [Fact]
        public async Task GetRegisteredCustomers_ShouldReturnMappedDtos()
        {
            var users = new List<User> { new User { Id = Guid.NewGuid(), UserName = "a", FullName = "A" } };
            var dtos = new List<UserDto> { new UserDto { Id = users[0].Id, UserName = "a", FullName = "A" } };

            _mockUserManager.Setup(um => um.GetUsersInRoleAsync("Customer")).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<List<UserDto>>(users)).Returns(dtos);

            var result = await _service.GetRegisteredCustomers();

            Assert.Single(result);
            Assert.Equal("A", result[0].FullName);
            _mockUserManager.Verify(um => um.GetUsersInRoleAsync("Customer"), Times.Once);
        }
    }
}
