using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                    p => p.ProductImages,
                    p => p.ProductVariants
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
                Description = productCommand.Description,
                Categories = categories
            };

            product.ProductVariants = productCommand.SizesQuantity.Select(sq => new ProductVariant
            {
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

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductCommand productCommand)
        {
            await _serviceProvider.ValidateAndThrowAsync(productCommand);

            var categories = await _unitOfWork.Categories.GetByIdsAsync(productCommand.CategoryIds);

            Product? product = await _unitOfWork.Products.GetWithConditionAndIncludeAsync(
                    p => p.Id == productCommand.Id,
                    p => p.Categories,
                    p => p.ProductImages,
                    p => p.ProductVariants
            );

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            //Update product properties
            product.ProductName = productCommand.ProductName;
            product.Price = productCommand.Price;
            product.Description = productCommand.Description;

            product.Categories = categories;

            //product.ProductVariants = productCommand.SizesQuantity;

            foreach (var req in productCommand.SizesQuantity)
            {
                var existing = product.ProductVariants.FirstOrDefault(pv => pv.Id == req.Id);

                if (existing != null)
                {
                    existing.StockQuantity = req.Quantity;
                }
                else
                {
                    _unitOfWork.ProductVariants.Add(new ProductVariant
                    {
                        ProductId = product.Id,
                        SizeId = req.SizeId,
                        StockQuantity = req.Quantity
                    });
                }
            }

            //Remove images if needed

            if (productCommand.RemoveImageIds?.Any() == true)
            {
                var imageToRemove = product.ProductImages.Where(pi => productCommand.RemoveImageIds.Contains(pi.Id)).ToList();

                _unitOfWork.ProductImages.RemoveRange(imageToRemove);
            }

            //Clear is thumbnail

            if (productCommand.ThumbnailImageId != null || productCommand.ThumbnailIndex != null)
            {
                foreach (var image in product.ProductImages)
                {
                    image.IsThumbnail = false;
                }
            }

            if (productCommand.ThumbnailImageId != null)
            {
                var thumb = product.ProductImages
                    .FirstOrDefault(x => x.Id == productCommand.ThumbnailImageId);

                if (thumb != null)
                {
                    thumb.IsThumbnail = true;
                }
            }

            if (productCommand.ProductImages != null && productCommand.ProductImages.Count > 0)
            {
                List<string>? imageUrls = await _cloudinaryService.UploadImages(productCommand.ProductImages, "products");

                if (imageUrls == null || imageUrls.Count == 0)
                {
                    throw new Exception("Failed to upload product images.");
                }

                var newImages = imageUrls.Select((url, index) => new ProductImage
                {
                    ProductId = productCommand.Id,
                    ImageUrl = url,
                    IsThumbnail = index == productCommand.ThumbnailIndex
                }).ToList();


                _unitOfWork.ProductImages.AddRange(newImages);
            }

            await _unitOfWork.CompleteAsync();


            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CompleteAsync();
        }
    }
}
