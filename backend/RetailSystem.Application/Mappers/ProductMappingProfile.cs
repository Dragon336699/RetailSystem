using AutoMapper;
using RetailSystem.Application.Dtos.Products;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<ProductImage, ProductImageDto>();
        }
    }
}
