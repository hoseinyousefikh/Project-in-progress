using App.Domain.AppServices.Home.AppServices.Users;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using DwellMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading;

namespace DwellMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly ILogger<AdminUserAppService> _logger;
        private readonly UserManager<User> _userManager;
        

        public AuthenticationController(IUserAppService userAppService, ICityAppService cityAppService, IAdminUserAppService adminUserAppService, UserManager<User> userManager, ILogger<AdminUserAppService> logger)
        {
            _userAppService = userAppService;
            _cityAppService = cityAppService;
            _adminUserAppService = adminUserAppService;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);

            ViewBag.Cities = new SelectList(cities, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userAppService.RegisterAsync(
                model.FirstName, model.LastName, model.Email, model.Password,
                model.ConfirmPassword, model.CityId, model.RoleType, cancellationToken);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return BadRequest(ModelState);
            }

            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userAppService.Login(model.Username, model.Password, model.RememberMe);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return BadRequest(ModelState);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var result = await _userAppService.UpdatePasswordAsync(
                userId, model.CurrentPassword, model.NewPassword, model.ConfirmPassword, cancellationToken);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "خطا در بروزرسانی رمز عبور");
                return View(model);
            }

            return RedirectToAction("Details", "Authentication");
        }


        [HttpGet]
        public IActionResult UpdateEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmail(UpdateEmailDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var result = await _userAppService.UpdateEmailAsync(userId, model.NewEmail, cancellationToken);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "خطا در بروزرسانی ایمیل");
                return View(model);
            }

            return RedirectToAction("Details", "Authentication");
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(); 
            }

            var user = await _adminUserAppService.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);
            ViewBag.Cities = new SelectList(cities, "Id", "Name");

            var model = new EditUserDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityId = user.CityId,
                ProfilePicture = user.ProfilePicture,
                Description = user.Description,
                Address = user.Address,
                ShebaNumber = user.ShebaNumber,
                CardNumber = user.CardNumber,
                RoleStatus = user.RoleType == RoleEnum.Customer
                    ? (user.CustomerDetails?.RoleStatus ?? UserStatus.inActive)
                    : (user.ExpertDetails?.RoleStatus ?? UserStatus.inActive)
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDto model, IFormFile profilePicture, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            UserStatus currentStatus = model.RoleStatus;

            if (currentStatus == null)
            {
                var currentUser = await _userManager.FindByIdAsync(userId.ToString());
                if (currentUser == null)
                {
                    ModelState.AddModelError("", "کاربر پیدا نشد.");
                    return View(model);
                }

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
            }

            var current = await _userManager.FindByIdAsync(userId.ToString());
            if (current == null)
            {
                ModelState.AddModelError("", "کاربر پیدا نشد.");
                return View(model);
            }

            model.ProfilePicture = await _adminUserAppService.EditProfileAndUploadImage(
                userId,
                profilePicture,
                current.ProfilePicture,
                cancellationToken
            );

            var result = await _userAppService.UpdateUserAsync(
                userId,
                model.FirstName,
                model.LastName,
                model.CityId,
                model.ProfilePicture,
                model.Description,
                model.Address,
                model.ShebaNumber,
                model.CardNumber,
                cancellationToken
            );

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Error updating user details.");
                return View(model);
            }

            return RedirectToAction("Details", "Authentication");
        }



        [HttpGet]
        public async Task<IActionResult> Details(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var user = await _adminUserAppService.GetByIdAsync(userId, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            if (user.CityId.HasValue)
            {
                user.City = await _cityAppService.GetCityByIdAsync(user.CityId.Value, cancellationToken);
            }

            var cityName = user?.City?.Name ?? "نامشخص";
          

            var model = new
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityName = cityName,
                ProfilePicture = user.ProfilePicture,
                Description = user.Description,
                Address = user.Address,
                ShebaNumber = user.ShebaNumber,
                CardNumber = user.CardNumber,
                Balance = user.Balance, 
                RoleStatus = user.RoleType == RoleEnum.Customer
                    ? (user.CustomerDetails?.RoleStatus ?? UserStatus.inActive)
                    : (user.ExpertDetails?.RoleStatus ?? UserStatus.inActive)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOutCustomer(CancellationToken cancellationToken)
        {
            try
            {
                await _userAppService.SignOutCustomerAsync();

                _logger.LogInformation("Customer signed out and redirected to profile details.");

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims. Redirecting to login.");
                    return RedirectToAction("Login", "Authentication");
                }

                return RedirectToAction("Index", "Home", new { userId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the sign out process.");
                return RedirectToAction("Error", "Home");
            }
        }


    }
}
