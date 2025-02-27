using App.Domain.Core.Home.Contract.Repositories.Categories;
using Dwell.Models;
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
        private readonly IHomeServiceRepository _homeServiceRepository;

        public HomeController(ILogger<HomeController> logger,
            ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository,
            IHomeServiceRepository homeServiceRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _homeServiceRepository = homeServiceRepository;
        }

        public IActionResult Index(CancellationToken cancellationToken)
        {
            //var result = await _categoryRepository.GetAllAsync(cancellationToken);
            //return View(result);
            return View();
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
