using RetailSystem.API.Contracts.Products;
using RetailSystem.Application.Dtos.ImagesUpload;
using RetailSystem.Application.Dtos.Products;

namespace RetailSystem.API.Extensions
{
    public static class ProductsExtensions
    {
        public static CreateProductCommand ToCreateProductCommand(this CreateProductRequest request)
        {
            return new CreateProductCommand
            {
                ProductName = request.ProductName,
                Price = request.Price,
                ColorId = request.ColorId,
                CategoryIds = request.CategoryIds,
                Description = request.Description,
                ProductImages = request.ProductImages.Select(file => new ImageUploadDto
                {
                    FileName = file.FileName,
                    Content = file.OpenReadStream(),
                    ContentType = file.ContentType
                }).ToList(),
                ThumbnailIndex = request.ThumbnailIndex,
                SizesQuantity = request.SizesQuantity
            };
        }

        public static UpdateProductCommand ToUpdateProductCommand(this UpdateProductRequest request)
        {
            return new UpdateProductCommand
            {
                Id = request.Id,
                ProductName = request.ProductName,
                Price = request.Price,
                ColorId = request.ColorId,
                Description = request.Description,
                CategoryIds = request.CategoryIds,
                ProductImages = request.ProductImages.Select(file => new ImageUploadDto
                {
                    FileName = file.FileName,
                    Content = file.OpenReadStream(),
                    ContentType = file.ContentType
                }).ToList(),
                RemoveImageIds = request.RemoveImageIds,
                ThumbnailIndex = request.ThumbnailIndex,
                SizesQuantity = request.SizesQuantity
            };
        }
    }
}
