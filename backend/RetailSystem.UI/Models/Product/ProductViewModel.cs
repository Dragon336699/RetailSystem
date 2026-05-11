using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.SharedLibrary.Dtos.Sizes;

namespace RetailSystem.UI.Models.Product
{
    public class ProductViewModel
    {
        public ProductDto Product { get; init; }
        public List<SizeDto> Sizes { get; init; }
    }
}
