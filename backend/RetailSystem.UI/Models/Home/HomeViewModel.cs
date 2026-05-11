using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.SharedLibrary.Dtos.Products;
namespace RetailSystem.UI.Models.Home
{
    public class HomeViewModel
    {
        public List<ProductDto> FeatureProducts { get; init; } = new List<ProductDto>();
        public IEnumerable<CategoryDto> Categories { get; init; } = new List<CategoryDto>();
    }
}
