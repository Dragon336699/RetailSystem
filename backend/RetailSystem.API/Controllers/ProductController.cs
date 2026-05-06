using Microsoft.AspNetCore.Mvc;
using RetailSystem.API.Contracts.Products;
using RetailSystem.API.Extensions;
using RetailSystem.Application.Dtos.Products;
using RetailSystem.Application.Interfaces.Services;

namespace RetailSystem.API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int skip = 0, int take = 10)
        {
            var products = await _productService.GetProductsAsync(skip, take);
            return Ok(products);
        }

        [HttpGet]
        [Route("/{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);

        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductRequest productRequest)
        {
            CreateProductCommand createProductCommand = productRequest.ToCommand();

            var productAdded = await _productService.AddProductAsync(createProductCommand);
            return Ok(productAdded);
        }
    }
}
