using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Entities.Categories;
using DwellMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeServiceController : Controller
    {
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ISubCategoryAppService _subCategoryAppService;
        private const int PageSize = 10;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeServiceController> _logger;

        public HomeServiceController(IHomeServiceAppService homeServiceAppService, IWebHostEnvironment webHostEnvironment, ISubCategoryAppService subCategoryAppService, ILogger<HomeServiceController> logger)
        {
            _homeServiceAppService = homeServiceAppService;
            _webHostEnvironment = webHostEnvironment;
            _subCategoryAppService = subCategoryAppService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? subCategoryId, int page = 1, CancellationToken cancellationToken = default)
        {
            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
            var allHomeServices = await _homeServiceAppService.GetAllHomeServicesAsync(cancellationToken);

            if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                allHomeServices = allHomeServices.Where(h => h.SubCategoryId == subCategoryId.Value).ToList();
            }

            var pagedServices = allHomeServices.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var viewModel = new HomeServiceIndexViewModel
            {
                SubCategories = subCategories,
                HomeServices = pagedServices,
                SelectedSubCategoryId = subCategoryId,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)allHomeServices.Count / PageSize)
            };
            _logger.LogInformation("ساب کتیگوری به ویو ارسال شد");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(CancellationToken.None);
            var viewModel = new HomeServiceCreateViewModel
            {
                SubCategories = subCategories
            };
            return View(viewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(HomeServiceCreateViewModel viewModel, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
        //        viewModel.SubCategories = subCategories;
        //        return View(viewModel);
        //    }

        //    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/homeservices");
        //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewModel.ImageFile.FileName);
        //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    Directory.CreateDirectory(uploadsFolder);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await viewModel.ImageFile.CopyToAsync(fileStream);
        //    }

        //    var homeService = new HomeService
        //    {
        //        Name = viewModel.Name,
        //        Description = viewModel.Description,
        //        BasePrice = viewModel.BasePrice,
        //        SubCategoryId = viewModel.SubCategoryId,
        //        ImageUrl = $"/images/homeservices/{uniqueFileName}",
        //        ViewCount = 0,
        //        IsDeleted = false
        //    };

        //    await _homeServiceAppService.AddHomeServiceAsync(homeService, cancellationToken);
        //    _logger.LogInformation("هوم سرویس با موفقیت اضافه شد");
        //    TempData["SuccessMessage"] = "هوم سرویس با موفقیت اضافه شد!";

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> Create(HomeServiceCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
                viewModel.SubCategories = subCategories;
                return View(viewModel);
            }

            string imageUrl = await _homeServiceAppService.AddImageToFileSystem(viewModel.ImageFile, "homeservices", cancellationToken);

            var homeService = new HomeService
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                BasePrice = viewModel.BasePrice,
                SubCategoryId = viewModel.SubCategoryId,
                ImageUrl = imageUrl,
                ViewCount = 0,
                IsDeleted = false
            };

            await _homeServiceAppService.AddHomeServiceAsync(homeService, cancellationToken);

            _logger.LogInformation("هوم سرویس با موفقیت اضافه شد");
            TempData["SuccessMessage"] = "هوم سرویس با موفقیت اضافه شد!";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetHomeServiceByIdAsync(id, cancellationToken);
            if (homeService == null)
                return NotFound();

            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);

            var viewModel = new HomeServiceEditViewModel
            {
                Id = homeService.Id,
                Name = homeService.Name,
                Description = homeService.Description,
                BasePrice = homeService.BasePrice,
                SubCategoryId = homeService.SubCategoryId,
                IsDeleted = homeService.IsDeleted,
                ViewCount = homeService.ViewCount,
                ImageUrl = homeService.ImageUrl,
                SubCategories = subCategories
            };

            return View(viewModel);
        }


        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, HomeServiceEditViewModel viewModel, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
        //        viewModel.SubCategories = subCategories;
        //        return View(viewModel); 
        //    }

        //    var existingService = await _homeServiceAppService.GetHomeServiceByIdAsync(id, cancellationToken);
        //    if (existingService == null)
        //        return NotFound();

        //    if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
        //    {
        //        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/homeservices");
        //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewModel.ImageFile.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        Directory.CreateDirectory(uploadsFolder);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await viewModel.ImageFile.CopyToAsync(fileStream); 
        //        }

        //        if (!string.IsNullOrEmpty(existingService.ImageUrl))
        //        {
        //            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingService.ImageUrl.TrimStart('/'));
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);  
        //            }
        //        }

        //        existingService.ImageUrl = $"/images/homeservices/{uniqueFileName}";  
        //    }
        //    else
        //    {
        //        existingService.ImageUrl = existingService.ImageUrl;
        //    }

        //    existingService.Name = viewModel.Name;
        //    existingService.Description = viewModel.Description;
        //    existingService.BasePrice = viewModel.BasePrice;
        //    existingService.SubCategoryId = viewModel.SubCategoryId;
        //    existingService.ViewCount = viewModel.ViewCount;
        //    existingService.IsDeleted = viewModel.IsDeleted;

        //    await _homeServiceAppService.UpdateHomeServiceAsync(existingService, cancellationToken);
        //    _logger.LogInformation("هوم سرویس با موفقیت اپدیت شد");
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HomeServiceEditViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
                viewModel.SubCategories = subCategories;
                return View(viewModel);
            }

            var existingService = await _homeServiceAppService.GetHomeServiceByIdAsync(id, cancellationToken);
            if (existingService == null)
                return NotFound();

            existingService.ImageUrl = await _homeServiceAppService.EditHomeServiceImage(viewModel.ImageFile, existingService.ImageUrl, cancellationToken);

            existingService.Name = viewModel.Name;
            existingService.Description = viewModel.Description;
            existingService.BasePrice = viewModel.BasePrice;
            existingService.SubCategoryId = viewModel.SubCategoryId;
            existingService.ViewCount = viewModel.ViewCount;
            existingService.IsDeleted = viewModel.IsDeleted;

            await _homeServiceAppService.UpdateHomeServiceAsync(existingService, cancellationToken);
            _logger.LogInformation("هوم سرویس با موفقیت اپدیت شد");

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetHomeServiceByIdAsync(id, cancellationToken);
            if (homeService == null)
                return NotFound();

            return View(homeService);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetHomeServiceByIdAsync(id, cancellationToken);
            if (homeService == null)
                return NotFound();

            if (!string.IsNullOrEmpty(homeService.ImageUrl))
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, homeService.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _homeServiceAppService.DeleteHomeServiceAsync(id, cancellationToken);
            _logger.LogWarning("هوم سرویس با موفقیت حذف شد");
            return RedirectToAction("Index");
        }
    }
}
