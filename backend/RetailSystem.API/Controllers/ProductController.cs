using Microsoft.AspNetCore.Mvc;
using RetailSystem.API.Contracts.Products;
using RetailSystem.API.Extensions;
using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

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
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetFilteredProducts(Guid categoryId, int skip = 0, int take = 10)
        {
            var products = await _productService.GetFilteredProducts(categoryId, skip, take);
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductRequest productRequest)
        {
            CreateProductCommand createProductCommand = productRequest.ToCreateProductCommand();

            var productAdded = await _productService.AddProductAsync(createProductCommand);
            return Ok(productAdded);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest productRequest)
        {
            UpdateProductCommand updateProductCommand = productRequest.ToUpdateProductCommand();

            var productAdded = await _productService.UpdateProductAsync(updateProductCommand);
            return Ok(productAdded);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
