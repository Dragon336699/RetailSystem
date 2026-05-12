namespace RetailSystem.SharedLibrary.Dtos.Categories
{
    public record CreateCategoryCommand
    {
        public required string CategoryName { get; init; }
        public string? Description { get; init; }
    }
}
