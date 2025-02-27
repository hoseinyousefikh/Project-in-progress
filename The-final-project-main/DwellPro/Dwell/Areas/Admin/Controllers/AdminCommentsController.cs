using App.Domain.Core.Home.Contract.AppServices.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCommentsController : Controller
    {
        private readonly IAdminCommentAppService _adminCommentAppService;
        private readonly ILogger<AdminCommentsController> _logger;

        public AdminCommentsController(IAdminCommentAppService adminCommentAppService, ILogger<AdminCommentsController> logger)
        {
            _adminCommentAppService = adminCommentAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var comments = await _adminCommentAppService.GetAllCommentsAsync(cancellationToken);

            var sortedComments = comments.OrderBy(c => c.Experts.User.FirstName).ThenBy(c => c.Experts.User.LastName).ToList();
            _logger.LogInformation("کامنت ها نمایش داده میشود");

            return View(sortedComments);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveComment(int id, CancellationToken cancellationToken)
        {
            var result = await _adminCommentAppService.ApproveCommentAsync(id, cancellationToken);
            if (result)
            {
                TempData["Message"] = "کامنت تایید شد.";
                _logger.LogInformation("کامنت تایید میشود ");

            }
            else
            {
                TempData["Message"] = "خطا در تایید کامنت.";
                _logger.LogError("کامنت تایید نشد! ");

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RejectComment(int id, CancellationToken cancellationToken)
        {
            var result = await _adminCommentAppService.RejectCommentAsync(id, cancellationToken);
            if (result)
            {
                TempData["Message"] = "کامنت رد شد.";
                _logger.LogInformation("کامنت رد شد  ");

            }
            else
            {
                TempData["Message"] = "خطا در رد کردن کامنت.";
                _logger.LogError("خطا در رد کردن کامنت.");

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var comment = await _adminCommentAppService.GetCommentDetailsAsync(id, cancellationToken);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }
    }
}
