using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using DwellMVC.Areas.Admin.CustomAuthorize;
using DwellMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class UserManagementController : Controller
    {
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly IUserAppService _userAppService;
        private readonly ICityAppService _cityAppService;
        private readonly ILogger<UserManagementController> _logger;


        public UserManagementController(IAdminUserAppService adminUserAppService, IUserAppService userAppService, ICityAppService cityAppService, ILogger<UserManagementController> logger)
        {
            _adminUserAppService = adminUserAppService;
            _userAppService = userAppService;
            _cityAppService = cityAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string userType, CancellationToken cancellationToken)
        {
            var users = await _adminUserAppService.GetAllAsync(cancellationToken);

            if (!string.IsNullOrEmpty(userType))
            {
                if (userType == RoleEnum.Customer.ToString())
                    users = users.Where(u => u.RoleType == RoleEnum.Customer).ToList();
                else if (userType == RoleEnum.Expert.ToString())
                    users = users.Where(u => u.RoleType == RoleEnum.Expert).ToList();
            }

            return View(users);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var model = new UserCreateViewModel(); 

            var cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);

            ViewBag.Cities = new SelectList(cities, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);

                ViewBag.Cities = new SelectList(cities, "Id", "Name");

                return View(model);
            }

            try
            {
                var result = await _userAppService.RegisterAsync(
                    model.FirstName,
                    model.LastName,
                    model.Email,
                    model.Password,
                    model.ConfirmPassword,
                    model.CityId,
                    model.RoleType,
                    cancellationToken
                );

                if (result.Succeeded)
                {
                    _logger.LogInformation("کاربر با موفقیت ثبت شد");
                    TempData["SuccessMessage"] = "کاربر با موفقیت ثبت شد.";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var citiesAfterError = await _cityAppService.GetAllCitiesAsync(cancellationToken);
            ViewBag.Cities = new SelectList(citiesAfterError, "Id", "Name");

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var user = await _adminUserAppService.GetByIdAsync(id, CancellationToken.None);

                var cities = await _cityAppService.GetAllCitiesAsync(CancellationToken.None);
                ViewBag.CityName = cities.FirstOrDefault(c => c.Id == user.CityId)?.Name;

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _adminUserAppService.GetByIdAsync(id, cancellationToken);

                if (user == null)
                {
                    _logger.LogWarning("کاربر پیدا نشد.");
                    TempData["ErrorMessage"] = "کاربر پیدا نشد.";
                    return RedirectToAction(nameof(Index));
                }

                var cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);

                ViewBag.Cities = new SelectList(cities, "Id", "Name");

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id)
            {
                _logger.LogError("شناسه کاربر نامعتبر است");
                TempData["ErrorMessage"] = "شناسه کاربر نامعتبر است";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var currentUser = await _adminUserAppService.GetByIdAsync(id, CancellationToken.None);
                if (currentUser == null)
                {
                    _logger.LogError("کاربر یافت نشد");
                    TempData["ErrorMessage"] = "کاربر یافت نشد.";
                    return RedirectToAction(nameof(Index));
                }

                UserStatus currentStatus;
                if (currentUser.RoleType == RoleEnum.Customer && currentUser.CustomerDetails != null)
                {
                    currentStatus = currentUser.CustomerDetails.RoleStatus;
                }
                else if (currentUser.RoleType == RoleEnum.Expert && currentUser.ExpertDetails != null)
                {
                    currentStatus = currentUser.ExpertDetails.RoleStatus;
                }
                else
                {
                    _logger.LogError("نقش کاربر نامعتبر است.");
                    TempData["ErrorMessage"] = "نقش کاربر نامعتبر است.";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _userAppService.UpdateUserAsync(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.CityId,
                    user.ProfilePicture,
                    user.Description,
                    user.Address,
                    user.ShebaNumber,
                    user.CardNumber,
                    currentStatus, 
                    CancellationToken.None
                );

                if (result.Succeeded)
                {
                    _logger.LogInformation("اطلاعات کاربر با موفقیت بروزرسانی شد.");
                    TempData["SuccessMessage"] = "اطلاعات کاربر با موفقیت بروزرسانی شد.";
                }
                else
                {
                    _logger.LogError("بروزرسانی اطلاعات کاربر با شکست مواجه شد.");
                    TempData["ErrorMessage"] = "بروزرسانی اطلاعات کاربر با شکست مواجه شد.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _adminUserAppService.GetByIdAsync(id, CancellationToken.None);
                if (user == null)
                {
                    _logger.LogError("کاربر یافت نشد.");
                    TempData["ErrorMessage"] = "کاربر یافت نشد.";
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _userAppService.DeleteUserAsync(id, CancellationToken.None);

                if (result.Succeeded)
                {
                    _logger.LogInformation("کاربر با موفقیت حذف شد.");
                    TempData["SuccessMessage"] = "کاربر با موفقیت حذف شد.";
                }
                else
                {
                    _logger.LogError("حذف کاربر با شکست مواجه شد.");
                    TempData["ErrorMessage"] = "حذف کاربر با شکست مواجه شد.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var user = await _adminUserAppService.GetByIdAsync(id, CancellationToken.None);

                if (user == null)
                {
                    _logger.LogError("کاربر پیدا نشد.");
                    TempData["ErrorMessage"] = "کاربر پیدا نشد.";
                    return RedirectToAction(nameof(Index));
                }

                UserStatus newStatus;

                if (user.RoleType == RoleEnum.Customer)
                {
                    newStatus = user.CustomerDetails.RoleStatus == UserStatus.Active ? UserStatus.inActive : UserStatus.Active;
                    user.CustomerDetails.RoleStatus = newStatus;
                }
                else if (user.RoleType == RoleEnum.Expert)
                {
                    newStatus = user.ExpertDetails.RoleStatus == UserStatus.Active ? UserStatus.inActive : UserStatus.Active;
                    user.ExpertDetails.RoleStatus = newStatus;
                }
                else
                {
                    _logger.LogError("نقش کاربر نامعتبر است.");
                    TempData["ErrorMessage"] = "نقش کاربر نامعتبر است.";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _userAppService.UpdateUserAsync(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.CityId,
                    user.ProfilePicture,
                    user.Description,
                    user.Address,
                    user.ShebaNumber,
                    user.CardNumber,
                    newStatus, 
                    CancellationToken.None
                );

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = user.RoleType == RoleEnum.Customer
                        ? (newStatus == UserStatus.Active ? "مشتری تایید شد." : "مشتری تایید لغو شد.")
                        : (newStatus == UserStatus.Active ? "کارشناس تایید شد." : "کارشناس تایید لغو شد.");
                }
                else
                {
                    _logger.LogError("تایید کاربر با شکست مواجه شد.");
                    TempData["ErrorMessage"] = "تایید کاربر با شکست مواجه شد.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }


    }
}
