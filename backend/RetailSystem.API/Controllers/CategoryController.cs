using Microsoft.AspNetCore.Mvc;
using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace RetailSystem.API.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategories([FromBody] CreateCategoryCommand request)
        {
            var categoriesAdded = await _categoryService.AddCategoryAsync(request);
            return Ok(categoriesAdded);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoy([FromBody] UpdateCategoryCommand request)
        {
            var categoryUpdated = await _categoryService.UpdateCategoryAsync(request);
            return Ok(categoryUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
