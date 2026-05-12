namespace RetailSystem.SharedLibrary.Dtos.Categories
{
    public record UpdateCategoryCommand
    {
        public Guid Id { get; init; }
        public required string CategoryName { get; init; }
        public string? Description { get; init; }
    }
}
