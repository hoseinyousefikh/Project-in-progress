using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Entities.Other;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DwellMVC.Controllers
{
    public class CommentByExpertController : Controller
    {

        private readonly IAdminUserAppService _adminUserAppService;
        private readonly ICommentAppService _commentAppService;
        public CommentByExpertController(IAdminUserAppService adminUserAppService, ICommentAppService commentAppService)
        {
            _adminUserAppService = adminUserAppService;
            _commentAppService = commentAppService;
        }


        [HttpGet]
        public async Task<IActionResult> CommentsExpert(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Account"); 
            }

            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);
            var currentUserExpert = experts.FirstOrDefault(expert => expert.User.Id == userId);

            if (currentUserExpert == null)
            {
                ViewBag.ErrorMessage = "اکسپرت مربوط به کاربر پیدا نشد.";
                return View("Error"); 
            }

            var comments = await _commentAppService.GetCommentsByExpertIdAsync(currentUserExpert.Id, cancellationToken);

            if (comments == null || !comments.Any())
            {
                ViewBag.Message = "هیچ نظری برای این کارشناس یافت نشد.";
                return View(new List<Comments>()); 
            }

            return View(comments); 
        }
    }
}
