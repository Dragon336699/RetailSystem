using Microsoft.AspNetCore.Identity;
using RetailSystem.Domain.Entities;
using RetailSystem.Application.Interfaces.Seeder;

namespace RetailSystem.Infrastructure.Persistence.Seed
{
    public class UserSeeder : IDataSeeder
    {
        private readonly UserManager<User> _userManager;
        public UserSeeder(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            string[] roles = { "Admin" };

            var existingUser = await _userManager.FindByNameAsync("Admin");

            if (existingUser != null)
            {
                return;
            }

            User user = new User
            {
                UserName = "Admin",
                FullName = "Admin",
            };

            string initialPassword = "Admin123";

            var result = await _userManager.CreateAsync(user, initialPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Seed admin fail");
            }

            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
