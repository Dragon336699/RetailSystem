using RetailSystem.Application.Dtos.Categories;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Dtos.Products
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
        public List<ProductImageDto> ProductImages { get; init; }
        public List<CategoryDto> Categories { get; init; }
    }
}
