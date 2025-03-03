using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Services.Home.Services.Categories;
using DwellMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ISubCategoryAppService _subCategoryAppService;
        private readonly IHomeServiceAppService _homeServiceAppService;
        public CategoryController(ISubCategoryAppService subCategoryAppService, IHomeServiceAppService homeServiceAppService)
        {
            _subCategoryAppService = subCategoryAppService;
            _homeServiceAppService = homeServiceAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
            var filteredSubCategories = subCategories
                .Where(sc => sc.CategoryId == categoryId && !sc.IsDeleted) 
                .Select(sc => new SubCategoryViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    ImageUrl = sc.ImageUrl,
                    CategoryId = sc.CategoryId
                })
                .ToList();
            return View(filteredSubCategories);
        }

        [HttpGet]
        public async Task<IActionResult> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        {
            var allHomeServices = await _homeServiceAppService.GetAllHomeServicesAsync(cancellationToken);
            var homeServices = allHomeServices
                .Where(hs => hs.SubCategoryId == subCategoryId && !hs.IsDeleted) 
                .Select(hs => new HomeServiceViewModel
                {
                    Id = hs.Id,
                    Name = hs.Name,
                    ImageUrl = hs.ImageUrl,
                    Description = hs.Description,
                    BasePrice = hs.BasePrice,
                    ViewCount = hs.ViewCount,
                    SubCategoryId = hs.SubCategoryId
                })
                .ToList();
            return View(homeServices);
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseViewCount(int serviceId, CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetHomeServiceByIdAsync(serviceId, cancellationToken);
            if (homeService != null)
            {
                homeService.ViewCount++; 
                await _homeServiceAppService.UpdateHomeServiceAsync(homeService, cancellationToken);
            }

            return Ok();
        }
    }

}
