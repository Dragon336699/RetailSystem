namespace RetailSystem.Application.Dtos.Categories
{
    public class UpdateCategoryCommand
    {
        public Guid Id { get; init; }
        public required string CategoryName { get; init; }
        public string? Description { get; init; }
    }
}
