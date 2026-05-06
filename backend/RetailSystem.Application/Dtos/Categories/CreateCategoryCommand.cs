namespace RetailSystem.Application.Dtos.Categories
{
    public class CreateCategoryCommand
    {
        public required string CategoryName { get; init; }
        public string? Description { get; init; }
    }
}
