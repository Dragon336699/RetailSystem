using RetailSystem.Application.Dtos.Products;

namespace RetailSystem.API.Contracts.Products
{
    public class CreateProductRequest
    {
        public required string ProductName { get; init; }
        public decimal Price { get; init; }
        public Guid ColorId { get; init; }
        public string? Description { get; init; }
        public required List<Guid> CategoryIds { get; init; }
        public required List<IFormFile> ProductImages { get; init; }
        public int ThumbnailIndex { get; init; }
        public required List<UploadProductSizeDto> SizesQuantity { get; init; }
    }
}
