using Microsoft.AspNetCore.Mvc;
using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.SharedLibrary.Dtos.Sizes;
using RetailSystem.UI.Models.Product;
using RetailSystem.UI.Services.Intefaces;
using System.Threading.Tasks;

namespace RetailSystem.UI.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly ISizeApiClient _sizeApiClient;
        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient, ISizeApiClient sizeApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
            _sizeApiClient = sizeApiClient;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryDto> categories = await _categoryApiClient.GetAllCategoriesAsync();
            List<ProductDto> products = await _productApiClient.GetAllProductsAsync();
            return View(new AllProductViewModel
            {
                Categories = categories,
                Products = products
            });
        }

        [HttpGet("detail/{id:guid}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            ProductDto product = await _productApiClient.GetProductByIdAsync(id);
            List<SizeDto> sizes = await _sizeApiClient.GetSizesAsync();
            return View(new ProductViewModel
            {
                Product = product,
                Sizes = sizes
            });
        }
    }
}
