using RetailSystem.SharedLibrary.Dtos.ImagesUpload;

namespace RetailSystem.SharedLibrary.Dtos.Products
{
    public class UpdateProductCommand
    {
        public Guid Id { get; init; }
        public required string ProductName { get; init; }
        public decimal Price { get; init; }
        public string? Description { get; init; }
        public int? ThumbnailIndex { get; init; }
        public Guid? ThumbnailImageId { get; init; }
        public required List<Guid> CategoryIds { get; init; }
        public required List<ImageUploadDto> ProductImages { get; init; }
        public List<Guid>? RemoveImageIds { get; init; }
        public required List<UploadProductSizeDto> SizesQuantity { get; init; }
    }
}
