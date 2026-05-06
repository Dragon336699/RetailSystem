using RetailSystem.Application.Dtos.ImagesUpload;

namespace RetailSystem.Application.Dtos.Products
{
    public class CreateProductCommand
    {
        public required string ProductName { get; init; }
        public decimal Price { get; init; }
        public Guid ColorId { get; init; }
        public int ThumbnailIndex { get; init; }
        public required List<Guid> CategoryIds { get; init; }
        public required List<ImageUploadDto> ProductImages { get; init; }
        public required List<UploadProductSizeDto> SizesQuantity { get; init; }
    }
}
