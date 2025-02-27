using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Categories;
using DwellMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryAppService categoryAppService, IWebHostEnvironment webHostEnvironment, ILogger<CategoryController> logger)
        {
            _categoryAppService = categoryAppService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            _logger.LogInformation("دسته بندی ها نمایش داده شد");
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
            return View(categories);
        }

        public IActionResult Create()
        {
            var viewModel = new CategoryViewModel();

            return View(viewModel);
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(CategoryViewModel model, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    string uniqueFileName = null;

        //    if (model.ImageFile != null && model.ImageFile.Length > 0)
        //    {
        //        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/categories");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        Directory.CreateDirectory(uploadsFolder);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await model.ImageFile.CopyToAsync(fileStream);
        //        }
        //    }

        //    var category = new Category
        //    {
        //        Name = model.Name,
        //        ImageUrl = uniqueFileName != null ? $"/images/categories/{uniqueFileName}" : null
        //    };

        //    await _categoryAppService.AddCategoryAsync(category, cancellationToken);
        //    _logger.LogInformation("دسته بندی با موفقیت اضافه شد ");
        //    TempData["SuccessMessage"] = "دسته‌بندی با موفقیت اضافه شد!";

        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            string imageUrl = null;

            if (model.ImageFile != null)
            {
                imageUrl = await _categoryAppService.UploadImage(model.ImageFile, "categories", cancellationToken);
            }

            var category = new Category
            {
                Name = model.Name,
                ImageUrl = imageUrl
            };

            await _categoryAppService.AddCategoryAsync(category, cancellationToken);

            TempData["SuccessMessage"] = "دسته‌بندی با موفقیت اضافه شد!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryAppService.GetCategoryByIdAsync(id, cancellationToken);
            if (category == null)
                return NotFound();

            var model = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };
            _logger.LogInformation("دسته بندی به ویو ارسال شد");
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, CategoryViewModel model, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var existingCategory = await _categoryAppService.GetCategoryByIdAsync(id, cancellationToken);
        //    if (existingCategory == null)
        //        return NotFound();

        //    if (model.ImageFile != null && model.ImageFile.Length > 0)
        //    {
        //        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/categories");
        //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        Directory.CreateDirectory(uploadsFolder);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await model.ImageFile.CopyToAsync(fileStream);
        //        }

        //        if (!string.IsNullOrEmpty(existingCategory.ImageUrl))
        //        {
        //            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingCategory.ImageUrl.TrimStart('/'));
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }
        //        }

        //        existingCategory.ImageUrl = $"/images/categories/{uniqueFileName}";
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(existingCategory.ImageUrl))
        //        {
        //            ModelState.AddModelError("ImageFile", "لطفا یک تصویر انتخاب کنید.");
        //            return View(model);
        //        }
        //    }

        //    existingCategory.Name = model.Name;

        //    await _categoryAppService.UpdateCategoryAsync(existingCategory, cancellationToken);
        //    _logger.LogInformation("دسته بندی با موفقیت ویرایش شد");
        //    TempData["SuccessMessage"] = "دسته‌بندی با موفقیت ویرایش شد!";

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryDTO model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingCategory = await _categoryAppService.GetCategoryByIdAsync(id, cancellationToken);
            if (existingCategory == null)
                return NotFound();

            await _categoryAppService.EditCategoryAsync(existingCategory, model, model.ImageFile, cancellationToken);

            _logger.LogInformation("دسته بندی با موفقیت ویرایش شد");
            TempData["SuccessMessage"] = "دسته‌بندی با موفقیت ویرایش شد!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryAppService.GetCategoryByIdAsync(id, cancellationToken);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteConfirmedCategory(int id, CancellationToken cancellationToken)
        {
            await _categoryAppService.DeleteCategoryAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
