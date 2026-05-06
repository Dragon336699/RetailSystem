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

        public async Task<List<ProductDto>> GetProductsAsync(int skip = 0, int take = 10)
        {
            var products = await _unitOfWork.Products.GetProductsAsync(skip, take);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetWithConditionAndIncludeAsync(
                    p => p.Id == id,
                    p => p.Categories,
                    p => p.ProductImages
             );

            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddProductAsync(CreateProductCommand productCommand)
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
                ColorId = productCommand.ColorId,
                ImageUrl = url,
                IsThumbnail = index == productCommand.ThumbnailIndex
            }).ToList();

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProductDto>(product);
        }
    }
}
