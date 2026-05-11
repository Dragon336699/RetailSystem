using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> AddCategoryAsync(CreateCategoryCommand command);
        Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryCommand command);
        Task DeleteCategoryAsync(Guid id);
    }
}
