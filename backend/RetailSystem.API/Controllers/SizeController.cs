using Microsoft.AspNetCore.Mvc;
using RetailSystem.Application.Interfaces.Services;

namespace RetailSystem.API.Controllers
{
    [ApiController]
    [Route("sizes")]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSizes()
        {
            var sizes = await _sizeService.GetAllSizesAsync();
            return Ok(sizes);
        }
    }
}
