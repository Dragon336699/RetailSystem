namespace RetailSystem.Application.Dtos.Products
{
    public class ProductImageDto
    {
        public required string ImageUrl { get; init; }
        public bool IsThumbnail { get; init; }
    }
}
