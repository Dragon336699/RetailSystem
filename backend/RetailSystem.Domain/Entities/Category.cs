namespace RetailSystem.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string CategoryName { get; set; }
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
