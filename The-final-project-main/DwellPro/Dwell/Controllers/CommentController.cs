using App.Domain.AppServices.Home.AppServices.Users;
using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
using DwellMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DwellMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentAppService _commentAppService;
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly IExpertProposalAppService _expertProposalAppService;
        private readonly IUserAppService _userAppService;
         
        public CommentController(ICommentAppService commentAppService, IAdminUserAppService adminUserAppService , IExpertProposalAppService expertProposalAppService, IUserAppService userAppService)
        {
            _commentAppService = commentAppService;
            _adminUserAppService = adminUserAppService;
            _expertProposalAppService = expertProposalAppService;
            _userAppService = userAppService;
        }
        public IActionResult SomeSuccessAction()
        {
            var successMessage = TempData["SuccessMessage"]?.ToString();
            return View(model: successMessage); 
        }

        [HttpGet]
        public async Task<IActionResult> GetComment(int proposalId, CancellationToken cancellationToken)
        {
            var existingComment = await _commentAppService.GetByProposalIdAsync(proposalId, cancellationToken);

            var model = new CommentViewModel
            {
                ExpertProposalId = proposalId,
                Text = existingComment?.Text ?? string.Empty,
                Rating = existingComment?.Rating ?? 0
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitComment(int expertProposalId, string text, double rating, CancellationToken cancellationToken)
        {
            if (rating < 1 || rating > 5)
            {
                ModelState.AddModelError(string.Empty, "امتیاز باید بین 1 تا 5 باشد.");
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
            {
                ModelState.AddModelError(string.Empty, "برای ارسال نظر باید وارد حساب کاربری خود شوید.");
                return RedirectToAction("Login", "Authentication");
            }

            var user = await _adminUserAppService.GetByIdAsync(parsedUserId, cancellationToken);
            if (user == null || user.CustomerDetails == null)
            {
                ModelState.AddModelError(string.Empty, "اطلاعات کاربر یافت نشد.");
                return RedirectToAction("Login", "Authentication");
            }

            int customerId = user.CustomerDetails.Id;

            var expertProposal = await _expertProposalAppService.GetExpertProposalByIdAsync(expertProposalId, cancellationToken);
            if (expertProposal == null)
            {
                ModelState.AddModelError(string.Empty, "پیشنهاد مورد نظر یافت نشد.");
                return View();
            }

            int expertId = expertProposal.ExpertId;

            var existingComment = await _commentAppService.GetByProposalIdAsync(expertProposalId, cancellationToken);

            bool isSuccess;

            if (existingComment != null)
            {
                existingComment.Text = text;
                existingComment.Rating = rating;
                existingComment.IsApproved = false;
                existingComment.ExpertId = expertId;

                isSuccess = await _commentAppService.UpdateCommentAsync(existingComment, cancellationToken);
               
            }
            else
            {
                var newComment = new Comments
                {
                    ExpertProposalId = expertProposalId,
                    CustomerId = customerId,
                    ExpertId = expertId,
                    Text = text,
                    Rating = rating,
                    CreatedAt = DateTime.UtcNow,
                    IsApproved = false
                };

                isSuccess = await _commentAppService.AddCommentAsync(newComment, cancellationToken);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "کامنت شما با موفقیت ثبت شد.";
                }
            }

            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "خطا در ثبت یا ویرایش کامنت.");
                return View();
            }

            var allCommentsForExpert = await _commentAppService.GetCommentsByExpertIdAsync(expertId, cancellationToken);
            if (allCommentsForExpert != null && allCommentsForExpert.Any())
            {
                double newAverageRating = allCommentsForExpert.Average(c => c.Rating);

                var expert = await _userAppService.GetExpertByIdAsync(expertId, cancellationToken);
                if (expert != null)
                {
                    expert.Rating = newAverageRating;
                    await _userAppService.UpdateExpertAsync(expert, cancellationToken);
                }
            }

            return RedirectToAction("SomeSuccessAction");
        }

    }
}
