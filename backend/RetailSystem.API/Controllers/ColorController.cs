using Microsoft.AspNetCore.Mvc;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Services;

namespace RetailSystem.API.Controllers
{
    [ApiController]
    [Route("colors")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllColors()
        {
            var colors = await _colorService.GetAllColorsAsync();
            return Ok(colors);
        }
    }
}
