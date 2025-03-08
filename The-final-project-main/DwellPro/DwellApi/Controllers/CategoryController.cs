using App.Domain.AppServices.Home.AppServices.Categories;
using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DwellApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ISubCategoryAppService _subCategoryAppService;
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ICategoryAppService _categoryAppService;
        public CategoryController(ISubCategoryAppService subCategoryAppService, IHomeServiceAppService homeServiceAppService, ICategoryAppService categoryAppService)
        {
            _subCategoryAppService = subCategoryAppService;
            _homeServiceAppService = homeServiceAppService;
            _categoryAppService = categoryAppService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);

            if (categories == null || !categories.Any())
            {
                return NotFound(new { Message = "هیچ دسته‌بندی‌ای یافت نشد." });
            }

            var categoryDtos = categories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return Ok(categoryDtos);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategoriesByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppService.GetFilteredSubCategoriesAsync(categoryId, cancellationToken);

            if (subCategories == null || !subCategories.Any())
            {
                return NotFound(new { Message = "زیر دسته‌ای برای این دسته‌بندی یافت نشد." });
            }

            var subCategoryDtos = subCategories.Select(subCategory => new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name
            }).ToList();

            return Ok(subCategoryDtos);
        }



        [HttpGet("homeservices/{subCategoryId}")]
        public async Task<ActionResult<IEnumerable<HomeServiceDto>>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        {
            var homeServices = await _homeServiceAppService.GetFilteredHomeServicesAsync(subCategoryId, cancellationToken);

            if (homeServices == null || !homeServices.Any())
            {
                return NotFound(new { Message = "خدمات خانگی یافت نشد." });
            }

            var homeServiceDtos = homeServices.Select(homeService => new HomeServiceDto
            {
                Id = homeService.Id,
                Name = homeService.Name,
                Description = homeService.Description,
                BasePrice = homeService.BasePrice
            }).ToList();

            return Ok(homeServiceDtos);
        }

    }
}
