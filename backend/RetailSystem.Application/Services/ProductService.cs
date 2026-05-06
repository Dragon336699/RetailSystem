using AutoMapper;
using RetailSystem.Application.Common.Validators;
using RetailSystem.Application.Dtos.Products;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public ProductService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<Product>> GetProductsAsync(int skip = 0, int take = 10)
        {
            return await _unitOfWork.Products.GetProductsAsync(skip, take);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task AddProductAsync(CreateProductCommand productCommand)
        {
            await _serviceProvider.ValidateAndThrowAsync(productCommand);

            List<string>? imageUrls = await _cloudinaryService.UploadImages(productCommand.ProductImages, "products");

            if (imageUrls == null || imageUrls.Count == 0)
            {
                throw new Exception("Failed to upload product images.");
            }

            var categories = await _unitOfWork.Categories.GetByIdsAsync(productCommand.CategoryIds);
            var product = new Product
            {
                ProductName = productCommand.ProductName,
                Price = productCommand.Price,
                Categories = categories
            };

            product.ProductVariants = productCommand.SizesQuantity.Select(sq => new ProductVariant
            {
                ColorId = productCommand.ColorId,
                SizeId = sq.SizeId,
                StockQuantity = sq.Quantity
            }).ToList();

            product.ProductImages = imageUrls.Select((url, index) => new ProductImage
            {
                ImageUrl = url,
                IsThumbnail = index == productCommand.ThumbnailIndex
            }).ToList();

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }
    }
}
