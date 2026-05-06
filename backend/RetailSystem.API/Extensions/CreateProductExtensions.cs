using RetailSystem.API.Contracts.Products;
using RetailSystem.Application.Dtos.ImagesUpload;
using RetailSystem.Application.Dtos.Products;

namespace RetailSystem.API.Extensions
{
    public static class CreateProductExtensions
    {
        public static CreateProductCommand ToCommand(this CreateProductRequest request)
        {
            return new CreateProductCommand
            {
                ProductName = request.ProductName,
                Price = request.Price,
                ColorId = request.ColorId,
                CategoryIds = request.CategoryIds,
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
    }
}
