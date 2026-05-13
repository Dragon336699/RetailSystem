using Microsoft.AspNetCore.Identity;
using RetailSystem.Domain.Entities;
using RetailSystem.Application.Interfaces.Seeder;

namespace RetailSystem.Infrastructure.Persistence.Seed
{
    public class RoleSeeder : IDataSeeder
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleSeeder(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            string[] roles = { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Role { Name = role });
                }
            }
        }
    }
}
