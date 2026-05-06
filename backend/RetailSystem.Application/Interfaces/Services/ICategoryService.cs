using RetailSystem.Application.Dtos.Categories;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Task<List<CategoryDto>> AddCategoryAsync(List<CreateCategoryCommand> command);
        Task UpdateCategoryAsync(UpdateCategoryCommand command);
        Task DeleteCategoryAsync(Guid id);
    }
}
