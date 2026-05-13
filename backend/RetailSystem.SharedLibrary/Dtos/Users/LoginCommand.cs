namespace RetailSystem.SharedLibrary.Dtos.Users
{
    public record LoginCommand
    {
        public string UserName { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
