using AutoMapper;
using RetailSystem.Application.Dtos.Categories;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Mappers
{
    internal class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
