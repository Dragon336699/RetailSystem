using RetailSystem.Application.Dtos.Products;

namespace RetailSystem.API.Contracts.Products
{
    public class UpdateProductRequest
    {
        public Guid Id { get; init; }
        public required string ProductName { get; init; }
        public decimal Price { get; init; }
        public Guid ColorId { get; init; }
        public required List<Guid> CategoryIds { get; init; }
        public required List<IFormFile> ProductImages { get; init; }
        public List<Guid>? RemoveImageIds { get; init; }
        public int ThumbnailIndex { get; init; }
        public required List<UploadProductSizeDto> SizesQuantity { get; init; }
    }
}
