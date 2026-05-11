using Microsoft.AspNetCore.Mvc;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.UI.Models;
using RetailSystem.UI.Models.Home;
using RetailSystem.UI.Services.Intefaces;
using System.Diagnostics;

namespace RetailSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public HomeController(ILogger<HomeController> logger, IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> featureProducts = await _productApiClient.GetFeatureProducts();
            IEnumerable<CategoryDto> categories = await _categoryApiClient.GetAllCategoriesAsync();
            var homeViewModel = new HomeViewModel
            {
                FeatureProducts = featureProducts,
                Categories = categories
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
