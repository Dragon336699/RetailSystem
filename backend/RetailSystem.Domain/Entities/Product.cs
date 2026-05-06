namespace RetailSystem.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string ProductName { get; set; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public decimal Price { get; set; }
        public decimal RatingAverage { get; set; } = 0;
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
