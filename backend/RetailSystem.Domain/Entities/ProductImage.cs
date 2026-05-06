namespace RetailSystem.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsThumbnail { get; set; } = false;
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
        public Product Product { get; set; } = null!;
        public Color Color { get; set; } = null!;
    }
}
