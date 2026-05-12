namespace RetailSystem.SharedLibrary.Dtos.Users
{
    public class RegisterCustomerCommand
    {
        public required string FullName { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
