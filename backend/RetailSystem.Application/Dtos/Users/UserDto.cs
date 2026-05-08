namespace RetailSystem.Application.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; } = null!;

    }
}
