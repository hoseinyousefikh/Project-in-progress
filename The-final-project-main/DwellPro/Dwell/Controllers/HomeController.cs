using App.Domain.AppServices.Home.AppServices.Categories;
using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Entities.Categories;
using Dwell.Models;
using DwellMVC.BackgroundServices;
using DwellMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Dwell.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly RandomHomeServicesUpdater _randomHomeServicesUpdater;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger,
            IHomeServiceAppService homeServiceAppService,
            ICategoryAppService categoryAppService,
            RandomHomeServicesUpdater randomHomeServicesUpdater,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _categoryAppService = categoryAppService;
            _homeServiceAppService = homeServiceAppService;
            _randomHomeServicesUpdater = randomHomeServicesUpdater;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            if (!_memoryCache.TryGetValue("Categories", out List<Category> categories))
            {
                _logger.LogInformation("دریافت دسته‌بندی‌ها از سرویس...");
                categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                if (categories == null)
                {
                    _logger.LogWarning("هیچ دسته‌بندی‌ای یافت نشد!");
                    return NotFound();
                }
                _memoryCache.Set("Categories", categories, cacheOptions);
            }
            else
            {
                _logger.LogInformation("دسته‌بندی‌ها از کش بارگذاری شدند.");
            }

            ViewData["Categories"] = categories;

            if (!_memoryCache.TryGetValue("AllHomeServices", out List<HomeService> allHomeServices))
            {
                _logger.LogInformation("دریافت لیست خدمات از سرویس...");
                allHomeServices = await _homeServiceAppService.GetAllHomeServicesAsync(cancellationToken);
                if (allHomeServices == null)
                {
                    _logger.LogWarning("هیچ خدمتی یافت نشد!");
                    return NotFound();
                }
                _memoryCache.Set("AllHomeServices", allHomeServices, cacheOptions);
            }
            else
            {
                _logger.LogInformation("لیست خدمات از کش بارگذاری شد.");
            }

            var topHomeServices = allHomeServices
                .OrderByDescending(hs => hs.ViewCount)
                .Take(5)
                .ToList();

            var latestHomeServices = allHomeServices
                .OrderByDescending(hs => hs.Id)
                .Take(2)
                .ToList();

            var randomHomeServices = await _randomHomeServicesUpdater.UpdateRandomHomeServices();

            var viewModel = new HomePageViewModel
            {
                Categories = categories,
                TopHomeServices = topHomeServices,
                LatestHomeServices = latestHomeServices,
                RandomHomeServices = randomHomeServices
            };

            return View(viewModel);
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
