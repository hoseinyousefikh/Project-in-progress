using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IExpertProposalAppService _expertProposalAppService;
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly ICityAppService _cityAppService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IOrderAppService orderAppService, IExpertProposalAppService expertProposalAppService, IAdminUserAppService adminUserAppService, ICityAppService cityAppService, ILogger<DashboardController> logger)
        {
            _orderAppService = orderAppService;
            _expertProposalAppService = expertProposalAppService;
            _adminUserAppService = adminUserAppService;
            _cityAppService = cityAppService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Logout()
        {

            return RedirectToAction("Login", "Admin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                _logger.LogInformation("Admin sign-out request received.");

                await _adminUserAppService.SignOutAdminAsync();

                _logger.LogInformation("Admin signed out successfully.");

                return RedirectToAction("Login", "Admin");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during admin sign-out.");
                TempData["ErrorMessage"] = "خطایی در خروج از حساب رخ داد.";
                return RedirectToAction("Index", "Dashboard");
            }
        }

        public async Task<IActionResult> Index()
        {
            var dashboardData = new
            {
                ExpertsCount = (await _adminUserAppService.GetAllAsync(CancellationToken.None)).Count(user => user.RoleType == RoleEnum.Expert),
                CustomersCount = (await _adminUserAppService.GetAllAsync(CancellationToken.None)).Count(user => user.RoleType == RoleEnum.Customer),
                ProposalsCount = (await _expertProposalAppService.GetAllExpertProposalsAsync(CancellationToken.None)).Count,
                OrdersCount = (await _orderAppService.GetAllOrdersAsync(CancellationToken.None)).Count
            };
            return View(dashboardData);
        }

        public async Task<IActionResult> Experts()
        {

            var experts = await _adminUserAppService.GetExpertsListAsync(CancellationToken.None);

            return View(experts);
        }

        public async Task<IActionResult> Customers()
        {
            var customers = await _adminUserAppService.GetCustomersListAsync(CancellationToken.None);
            return View(customers);
        }

        public async Task<IActionResult> Proposals()
        {
            var proposals = await _expertProposalAppService.GetAllExpertProposalsAsync(CancellationToken.None);
            return View(proposals);
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await _orderAppService.GetAllOrdersAsync(CancellationToken.None);
            return View(orders);
        }

        public async Task<IActionResult> RecentOrders()
        {
            var recentOrders = (await _orderAppService.GetAllOrdersAsync(CancellationToken.None)).Take(10).ToList();
            return View(recentOrders);
        }

        public async Task<IActionResult> RecentProposals()
        {
            var recentProposals = (await _expertProposalAppService.GetAllExpertProposalsAsync(CancellationToken.None)).Take(10).ToList();
            return View(recentProposals);
        }

        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _adminUserAppService.GetByIdAsync(1, CancellationToken.None);

                if (user?.CityId.HasValue == true)
                {
                    user.City = await _cityAppService.GetCityByIdAsync(user.CityId.Value, CancellationToken.None);
                }
                _logger.LogInformation("پروفایل بارگزاری شد");
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("خطادر بارگزاری فایل");
                TempData["ErrorMessage"] = $"خطا در بارگذاری پروفایل: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePro()
        {
            try
            {
                var user = await _adminUserAppService.GetByIdAsync(1, CancellationToken.None);
                _logger.LogInformation("بارگزاری پروفایل در قسمت اپدیت به خوبی انجام شد");
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("خطا در بارگزاری پروفایل ");
                TempData["ErrorMessage"] = $"خطا در بارگذاری پروفایل: {ex.Message}";
                return RedirectToAction("Profile");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdatePro(User user, IFormFile profilePicture)
        //{
        //    try
        //    {

        //        var existingUser = await _adminUserAppService.GetByIdAsync(1, CancellationToken.None);
        //        if (existingUser == null)
        //        {
        //            _logger.LogError("کاربر یافت نشد");
        //            TempData["ErrorMessage"] = "کاربر یافت نشد.";
        //            return RedirectToAction("Profile");
        //        }
        //        if (profilePicture != null && profilePicture.Length > 0)
        //        {
        //            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        //            if (!Directory.Exists(uploadsFolder))
        //            {
        //                Directory.CreateDirectory(uploadsFolder);
        //            }

        //            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);

        //            var filePath = Path.Combine(uploadsFolder, fileName);

        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await profilePicture.CopyToAsync(fileStream);
        //            }

        //            user.ProfilePicture = "/uploads/" + fileName;
        //        }
        //        else
        //        {
        //            user.ProfilePicture = existingUser.ProfilePicture;
        //        }

        //        var result = await _adminUserAppService.UpdateAdminAsync(
        //            user.Id=1,
        //            user.FirstName,
        //            user.LastName,
        //            user.CityId,
        //            user.ProfilePicture,
        //            user.Description,
        //            user.Address,
        //            user.ShebaNumber,
        //            user.CardNumber,
        //            CancellationToken.None);

        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("پروفایل با موفقیت به روز رسانی شد.");
        //            TempData["SuccessMessage"] = "پروفایل با موفقیت به روز رسانی شد.";
        //            return RedirectToAction("Profile");
        //        }
        //        _logger.LogError("به روز رسانی پروفایل با مشکل مواجه شد");
        //        TempData["ErrorMessage"] = "به روز رسانی پروفایل با مشکل مواجه شد.";
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("خطا در به روز رسانی پروفایل");
        //        TempData["ErrorMessage"] = $"خطا در به روز رسانی پروفایل: {ex.Message}";
        //    }

        //    return View("Profile", user);
        //}
        [HttpPost]
        public async Task<IActionResult> UpdatePro(User user, IFormFile profilePicture)
        {
            try
            {
                var existingUser = await _adminUserAppService.GetByIdAsync(1, CancellationToken.None);
                if (existingUser == null)
                {
                    _logger.LogError("کاربر یافت نشد");
                    TempData["ErrorMessage"] = "کاربر یافت نشد.";
                    return RedirectToAction("Profile");
                }

                user.ProfilePicture = await _adminUserAppService.EditProfileAndUploadImage(1, profilePicture, existingUser.ProfilePicture, CancellationToken.None);

                var result = await _adminUserAppService.UpdateAdminAsync(
                    user.Id = 1,
                    user.FirstName,
                    user.LastName,
                    user.CityId,
                    user.ProfilePicture,
                    user.Description,
                    user.Address,
                    user.ShebaNumber,
                    user.CardNumber,
                    CancellationToken.None
                );

                if (result.Succeeded)
                {
                    _logger.LogInformation("پروفایل با موفقیت به روز رسانی شد.");
                    TempData["SuccessMessage"] = "پروفایل با موفقیت به روز رسانی شد.";
                    return RedirectToAction("Profile");
                }

                _logger.LogError("به روز رسانی پروفایل با مشکل مواجه شد");
                TempData["ErrorMessage"] = "به روز رسانی پروفایل با مشکل مواجه شد.";
            }
            catch (Exception ex)
            {
                _logger.LogError("خطا در به روز رسانی پروفایل: " + ex.Message);
                TempData["ErrorMessage"] = $"خطا در به روز رسانی پروفایل: {ex.Message}";
            }

            return View("Profile", user);
        }



        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                _logger.LogWarning("پسورد جدید و تایید پسورد باید مشابه باشند.");
                TempData["ErrorMessage"] = "پسورد جدید و تایید پسورد باید مشابه باشند.";
                return View();
            }

            var result = await _adminUserAppService.UpdateAdminPasswordAsync(1, currentPassword, newPassword, CancellationToken.None);

            if (result.Succeeded)
            {
                _logger.LogInformation("پسورد با موفقیت تغییر کرد");
                TempData["SuccessMessage"] = "پسورد با موفقیت تغییر کرد.";
                return RedirectToAction("Profile");
            }
            _logger.LogError("تغییر پسورد با مشکل مواجه شد");
            TempData["ErrorMessage"] = "تغییر پسورد با مشکل مواجه شد.";
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ChangeEmail()
        {
            try
            {
                var user = await _adminUserAppService.GetByIdAsync(1, CancellationToken.None);
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("خطا در بارگذاری ایمیل");
                TempData["ErrorMessage"] = $"خطا در بارگذاری ایمیل: {ex.Message}";
                return RedirectToAction("Profile");
            }
        }


        [HttpPost]
        public async Task<IActionResult> ChangeEmail(string newEmail)
        {
            var result = await _adminUserAppService.UpdateAdminEmailAsync(1, newEmail, CancellationToken.None);
            if (result.Succeeded)
            {
                _logger.LogInformation("یمیل با موفقیت تغییر کرد");
                TempData["SuccessMessage"] = "ایمیل با موفقیت تغییر کرد.";
                return RedirectToAction("Profile");
            }
            _logger.LogError("تغییر ایمیل با مشکل مواجه شد");
            TempData["ErrorMessage"] = "تغییر ایمیل با مشکل مواجه شد.";
            return RedirectToAction("Profile");
        }
    }
}
