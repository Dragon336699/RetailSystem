namespace RetailSystem.Application.Dtos.Products
{
    public class ProductImageDto
    {
        public Guid Id { get; init; }
        public required string ImageUrl { get; init; }
        public bool IsThumbnail { get; init; }
    }
}
