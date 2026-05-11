using RetailSystem.SharedLibrary.Dtos.ImagesUpload;

namespace RetailSystem.SharedLibrary.Dtos.Products
{
    public class CreateProductCommand
    {
        public required string ProductName { get; init; }
        public decimal Price { get; init; }
        public string? Description { get; init; }
        public int ThumbnailIndex { get; init; }
        public required List<Guid> CategoryIds { get; init; }
        public required List<ImageUploadDto> ProductImages { get; init; }
        public required List<UploadProductSizeDto> SizesQuantity { get; init; }
    }
}
