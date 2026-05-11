using RetailSystem.SharedLibrary.Dtos.Categories;

namespace RetailSystem.UI.Services.Intefaces
{
    public interface ICategoryApiClient
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    }
}
