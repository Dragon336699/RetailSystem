using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.SharedLibrary.Dtos.Products;

namespace RetailSystem.UI.Models.Product
{
    public class AllProductViewModel
    {
        public List<ProductDto> Products { get; init; } = new List<ProductDto>();
        public IEnumerable<CategoryDto> Categories { get; init; } = new List<CategoryDto>();
    }
}
