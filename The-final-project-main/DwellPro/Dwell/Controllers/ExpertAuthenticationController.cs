using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DwellMVC.Controllers
{
    public class ExpertAuthenticationController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IExpertHomeServiceAppService _expertHomeServiceAppService;
        private readonly IAdminUserAppService _adminUserAppService;
        public ExpertAuthenticationController(IUserAppService userAppService, ICityAppService cityAppService, IHomeServiceAppService homeServiceAppService, IExpertHomeServiceAppService expertHomeServiceAppService, IAdminUserAppService adminUserAppService)
        {
            _userAppService = userAppService;
            _cityAppService = cityAppService;
            _homeServiceAppService = homeServiceAppService;
            _expertHomeServiceAppService = expertHomeServiceAppService;
            _adminUserAppService = adminUserAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetExpertDetailsAsync(int expertId, CancellationToken cancellationToken)
        {
            var expertDetails = await _userAppService.GetExpertDetailsAsync(expertId, cancellationToken);

            if (expertDetails == null)
            {
                return NotFound(new { message = "Expert not found." });
            }

            return Ok(expertDetails);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);
            ViewBag.Cities = new SelectList(cities, "Id", "Name");

            var homeServices = await _homeServiceAppService.GetAllHomeServicesAsync(cancellationToken);
            ViewBag.HomeServices = new SelectList(homeServices, "Id", "Name");

            var userModel = await _userAppService.GetEditUserDataAsync(userId, cancellationToken);
            if (userModel == null)
            {
                return NotFound("کاربر پیدا نشد.");
            }
            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);

            var currentUserExpert = experts.FirstOrDefault(expert => expert.User.Id == userId);
            if (currentUserExpert == null)
            {
                return NotFound("اکسپرت مربوط به کاربر پیدا نشد.");
            }

            var expertHomeServices = await _userAppService.GetHomeServiceByExpertIdAsync(currentUserExpert.Id, cancellationToken);
            if (expertHomeServices == null || !expertHomeServices.Any())
            {
                ViewBag.ErrorMessage = "هیچ هوم سرویسی برای اکسپرت انتخاب نشده است.";
                return View();
            }

            ViewBag.Expert = expertHomeServices.ToList();  

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDto model, IFormFile profilePicture, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int Id))
            {
                return Unauthorized();
            }

            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);
            var currentUserExpert = experts.FirstOrDefault(expert => expert.User.Id == Id);
            if (currentUserExpert == null)
            {
                return NotFound("اکسپرت مربوط به کاربر پیدا نشد.");
            }

            var currentExpertHomeServices = await _userAppService.GetHomeServiceByExpertIdAsync(currentUserExpert.Id, cancellationToken);
            if (currentExpertHomeServices == null)
            {
                return NotFound("اکسپرت هوم سرویس پیدا نشد.");
            }





            var selectedHomeServiceIds = model.SelectedHomeServiceIds ?? new List<int>();
            if (selectedHomeServiceIds.Count > 5)
            {
                ModelState.AddModelError("", "تعداد هوم سرویس‌ها نباید بیشتر از 5 باشد.");
                return View(model);
            }




            var homeServicesToRemove = model.RemovedHomeServiceIds ?? new List<int>();
            foreach (var homeServiceId in homeServicesToRemove)
            {
                var homeService = currentExpertHomeServices.FirstOrDefault(ehs => ehs.Id == homeServiceId);
                if (homeService != null)
                {
                    await _expertHomeServiceAppService.RemoveExpertHomeServiceAsync(homeServiceId, cancellationToken);
                }
            }




            var homeServicesToAdd = selectedHomeServiceIds
                .Except(currentExpertHomeServices.Select(ehs => ehs.HomeServiceId))
                .ToList();

            foreach (var homeServiceId in homeServicesToAdd)
            {
                await _expertHomeServiceAppService.AddExpertHomeServiceAsync(currentUserExpert.Id, homeServiceId, cancellationToken);
            }

            var result = await _userAppService.EditUserAsync(Id, model, profilePicture, cancellationToken);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "خطا در به‌روزرسانی اطلاعات کاربر.");
                return View(model);
            }

            return RedirectToAction("ExpertDetails", "ExpertAuthentication");
        }
        [HttpGet]
        public async Task<IActionResult> ExpertDetails(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var userModel = await _userAppService.GetUserDetailsAsync(userId, cancellationToken);

            if (userModel == null)
            {
                return NotFound();
            }
             
            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);

            var currentUserExpert = experts.FirstOrDefault(expert => expert.User.Id == userId);
            if (currentUserExpert == null)
            {
                return NotFound("اکسپرت مربوط به کاربر پیدا نشد.");
            }
            ViewBag.Rating = currentUserExpert.Rating;
            var expertHomeServices = await _userAppService.GetHomeServiceByExpertIdAsync(currentUserExpert.Id, cancellationToken);
            if (expertHomeServices == null || !expertHomeServices.Any())
            {
                ViewBag.ErrorMessage = "هیچ هوم سرویسی برای اکسپرت انتخاب نشده است.";
                return View(userModel);
            }

            ViewBag.Expert = expertHomeServices.ToList();

            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> ExpertDetailsByCustomer(int expertId, CancellationToken cancellationToken)
        {
            var expertDetails = await _userAppService.GetExpertDetailsAsync(expertId, cancellationToken);

            if (expertDetails == null)
            {
                return NotFound("اکسپرت مورد نظر یافت نشد.");
            }

            ViewBag.Rating = expertDetails.Rating;

            return View(expertDetails);
        }


    }
}
