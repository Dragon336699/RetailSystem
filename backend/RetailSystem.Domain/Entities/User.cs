using Microsoft.AspNetCore.Identity;

namespace RetailSystem.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public required string FullName { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public Cart Cart { get; set; } = null!;
    }
}