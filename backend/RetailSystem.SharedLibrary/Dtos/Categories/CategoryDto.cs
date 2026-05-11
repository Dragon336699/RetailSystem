namespace RetailSystem.SharedLibrary.Dtos.Categories
{
    public class CategoryDto
    {
        public Guid Id { get; init; }
        public string CategoryName { get; init; } = null!;
        public string? Description { get; init; }
    }
}
