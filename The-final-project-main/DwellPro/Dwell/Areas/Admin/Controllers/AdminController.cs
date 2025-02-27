using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Entities.Users;
using DwellMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminUserAppService adminUserAppService, SignInManager<User> signInManager, ILogger<AdminController> logger)
        {
            _adminUserAppService = adminUserAppService;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("مدل استیت رعایت نشده ");
                return View(model);

            }

            var result = await _adminUserAppService.LoginAdminAsync(model.Username, model.Password, model.RememberMe);

            if (result.Succeeded)
            {
                _logger.LogInformation("ورود موفقیت امیز بود");
                return RedirectToAction("Index", "Dashboard");
            }
            _logger.LogError("ورود کاربر با مشکل مواجه شد");
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
    }
}
