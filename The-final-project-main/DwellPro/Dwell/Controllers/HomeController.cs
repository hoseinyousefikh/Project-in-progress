using App.Domain.AppServices.Home.AppServices.Categories;
using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.Repositories.Categories;
using Dwell.Models;
using DwellMVC.BackgroundServices;
using DwellMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Dwell.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly RandomHomeServicesUpdater _randomHomeServicesUpdater;



        public HomeController(ILogger<HomeController> logger,
            ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository,IHomeServiceAppService homeServiceAppService
           , ICategoryAppService categoryAppService, RandomHomeServicesUpdater randomHomeServicesUpdater)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _categoryAppService = categoryAppService;
            _homeServiceAppService = homeServiceAppService;
            _randomHomeServicesUpdater = randomHomeServicesUpdater;

        }


        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);

            if (categories == null)
            {
                return NotFound();
            }
            ViewData["Categories"] = categories;

            var allHomeServices = await _homeServiceAppService.GetAllHomeServicesAsync(cancellationToken);

            if (allHomeServices == null)
            {
                return NotFound();
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

        public ActionResult Details()
        {
            ViewBag.Message = "این صفحه جزئیات کارت است.";

            return View();
        }
        public IActionResult GetAllHomeService(CancellationToken cancellationToken)
        {
          
            return View();
        }
        //var result = await _homeServiceRepository.GetAllAsync(cancellationToken);
        //return View(result);
        public IActionResult GetAllSubCategory(CancellationToken cancellationToken)
        {
            //var result = await _subCategoryRepository.GetAllAsync(cancellationToken);
            //return View(result);
            return View();

        }
        public IActionResult Shop(CancellationToken cancellationToken)
        {

            return View();
        }
        public IActionResult listOrders()
        {
            return View();
        }
        public IActionResult ShopList()
        {
            return View();
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
