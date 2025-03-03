using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Services.Home.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> ViewCategory(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _categoryAppService.GetCategoryByIdAsync(categoryId, cancellationToken);

            if (category == null)
            {
                return NotFound();
            }

            return RedirectToAction("ViewSubCategories", "SubCategory", new { categoryId = category.Id });
        }
    }

}
