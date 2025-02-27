using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.Entities.Categories;
using DwellMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryAppService _subCategoryAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<SubCategoryController> _logger;


        public SubCategoryController(ISubCategoryAppService subCategoryAppService, ICategoryAppService categoryAppService, IWebHostEnvironment webHostEnvironment, ILogger<SubCategoryController> logger)
        {
            _subCategoryAppService = subCategoryAppService;
            _categoryAppService = categoryAppService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? categoryId, CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                subCategories = subCategories.Where(s => s.CategoryId == categoryId.Value).ToList();
            }

            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;

            return View(subCategories);
        }


        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var subCategory = await _subCategoryAppService.GetSubCategoryByIdAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound();
            return View(subCategory);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(SubCategoryViewModel model, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
        //        return View(model);
        //    }

        //    string uniqueFileName = null;
        //    if (model.ImageFile != null && model.ImageFile.Length > 0)
        //    {
        //        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/subcategories");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        Directory.CreateDirectory(uploadsFolder);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await model.ImageFile.CopyToAsync(fileStream);
        //        }
        //    }

        //    var subCategory = new SubCategory
        //    {
        //        Name = model.Name,
        //        ImageUrl = uniqueFileName != null ? $"/images/subcategories/{uniqueFileName}" : null,
        //        CategoryId = model.CategoryId
        //    };

        //    await _subCategoryAppService.AddSubCategoryAsync(subCategory, cancellationToken);
        //    _logger.LogInformation("زیرمجموعه با موفقیت اضافه شد");
        //    TempData["SuccessMessage"] = "زیرمجموعه با موفقیت اضافه شد!";

        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }

            string imageUrl = string.Empty;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                imageUrl = await _subCategoryAppService.AddImageSubCategory(model.ImageFile, "subcategories", cancellationToken);
            }

            var subCategory = new SubCategory
            {
                Name = model.Name,
                ImageUrl = !string.IsNullOrEmpty(imageUrl) ? imageUrl : null,
                CategoryId = model.CategoryId
            };

            await _subCategoryAppService.AddSubCategoryAsync(subCategory, cancellationToken);
            _logger.LogInformation("زیرمجموعه با موفقیت اضافه شد");
            TempData["SuccessMessage"] = "زیرمجموعه با موفقیت اضافه شد!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var subCategory = await _subCategoryAppService.GetSubCategoryByIdAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound();

            var model = new SubCategoryViewModel
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                ImageUrl = subCategory.ImageUrl
            };

            ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, SubCategoryViewModel model, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
        //        return View(model);
        //    }

        //    var existingSubCategory = await _subCategoryAppService.GetSubCategoryByIdAsync(id, cancellationToken);
        //    if (existingSubCategory == null)
        //        return NotFound();

        //    if (model.ImageFile != null && model.ImageFile.Length > 0)
        //    {
        //        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/subcategories");
        //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        Directory.CreateDirectory(uploadsFolder);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await model.ImageFile.CopyToAsync(fileStream);
        //        }

        //        if (!string.IsNullOrEmpty(existingSubCategory.ImageUrl))
        //        {
        //            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingSubCategory.ImageUrl.TrimStart('/'));
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }
        //        }

        //        existingSubCategory.ImageUrl = $"/images/subcategories/{uniqueFileName}";
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(existingSubCategory.ImageUrl))
        //        {
        //            ModelState.AddModelError("ImageFile", "لطفا یک تصویر انتخاب کنید.");
        //            ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
        //            return View(model);
        //        }
        //    }

        //    existingSubCategory.Name = model.Name;
        //    existingSubCategory.CategoryId = model.CategoryId;

        //    await _subCategoryAppService.UpdateSubCategoryAsync(existingSubCategory, cancellationToken);
        //    _logger.LogInformation("زیرمجموعه با موفقیت ویرایش شد!");
        //    TempData["SuccessMessage"] = "زیرمجموعه با موفقیت ویرایش شد!";

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SubCategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }

            var existingSubCategory = await _subCategoryAppService.GetSubCategoryByIdAsync(id, cancellationToken);
            if (existingSubCategory == null)
                return NotFound();

            string updatedImageUrl = await _subCategoryAppService.EditSubCategoryImage(model.ImageFile, existingSubCategory.ImageUrl, cancellationToken);

            existingSubCategory.ImageUrl = updatedImageUrl;
            existingSubCategory.Name = model.Name;
            existingSubCategory.CategoryId = model.CategoryId;

            await _subCategoryAppService.UpdateSubCategoryAsync(existingSubCategory, cancellationToken);
            _logger.LogInformation("زیرمجموعه با موفقیت ویرایش شد!");
            TempData["SuccessMessage"] = "زیرمجموعه با موفقیت ویرایش شد!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            var subCategory = await _subCategoryAppService.GetSubCategoryByIdAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound();

            return View(subCategory);
        }


        [HttpPost, ActionName("DeleteSubCategory")]
        public async Task<IActionResult> DeleteCDeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            var subCategory = await _subCategoryAppService.GetSubCategoryByIdAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound(); 

            if (!string.IsNullOrEmpty(subCategory.ImageUrl))
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, subCategory.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath); 
                }
            }

            await _subCategoryAppService.DeleteSubCategoryAsync(id, cancellationToken);

            return RedirectToAction("Index"); 
        }

    }
}
