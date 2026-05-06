using Microsoft.AspNetCore.Mvc;
using RetailSystem.Application.Dtos.Categories;
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
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> GetCategory([FromBody] List<CreateCategoryCommand> request)
        {
            await _categoryService.AddCategoryAsync(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoy([FromBody] UpdateCategoryCommand request)
        {
            await _categoryService.UpdateCategoryAsync(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
