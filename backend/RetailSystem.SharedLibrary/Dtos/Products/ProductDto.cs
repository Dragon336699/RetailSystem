using RetailSystem.SharedLibrary.Dtos.Categories;

namespace RetailSystem.SharedLibrary.Dtos.Products
{
    public class ProductDto
    {
        public Guid Id { get; init; }
        public required string ProductName { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public decimal RatingAverage { get; init; }
        public List<ProductImageDto> ProductImages { get; init; } = null!;
        public List<CategoryDto> Categories { get; init; } = null!;
        public List<ProductVariantDto> ProductVariants { get; init; } = null!;
    }
}
