using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Other;
using System.Security.Claims;

namespace App.Domain.AppServices.Home.AppServices.Other
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IAdminUserService _adminUserService;
        private readonly IExpertProposalService _expertProposalService;
        public CommentAppService(ICommentService commentService, IUserService userService, IAdminUserService adminUserService, IExpertProposalService expertProposalService)
        {
            _commentService = commentService;
            _userService = userService;
            _adminUserService = adminUserService;
            _expertProposalService = expertProposalService;
        }
        public Task<bool> AddCommentAsync(Comments comment, CancellationToken cancellationToken)
        {
            return _commentService.AddCommentAsync(comment, cancellationToken);
        }

        public Task<Comments?> GetByProposalIdAsync(int expertProposalId, CancellationToken cancellationToken)
        {
            return _commentService.GetByProposalIdAsync(expertProposalId, cancellationToken);
        }

        public Task<List<Comments>> GetCommentsByExpertIdAsync(int expertId, CancellationToken cancellationToken)
        {
            return _commentService.GetCommentsByExpertIdAsync(expertId , cancellationToken);
        }

        public Task<bool> UpdateCommentAsync(Comments comment, CancellationToken cancellationToken)
        {
            return _commentService.UpdateCommentAsync(comment, cancellationToken);
        }
        public async Task<ResultDto> SubmitCommentAsync(int expertProposalId, string text, double rating, ClaimsPrincipal user, CancellationToken cancellationToken)
        {
            var result = new ResultDto();

            if (rating < 1 || rating > 5)
            {
                result.Succeeded = false;
                result.Message = "امتیاز باید بین 1 تا 5 باشد.";
                return result;
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
            {
                result.Succeeded = false;
                result.Message = "برای ارسال نظر باید وارد حساب کاربری خود شوید.";
                return result;
            }

            var userInfo = await _adminUserService.GetByIdAsync(parsedUserId, cancellationToken);
            if (userInfo == null || userInfo.CustomerDetails == null)
            {
                result.Succeeded = false;
                result.Message = "اطلاعات کاربر یافت نشد.";
                return result;
            }

            int customerId = userInfo.CustomerDetails.Id;

            var expertProposal = await _expertProposalService.GetExpertProposalByIdAsync(expertProposalId, cancellationToken);
            if (expertProposal == null)
            {
                result.Succeeded = false;
                result.Message = "پیشنهاد مورد نظر یافت نشد.";
                return result;
            }

            int expertId = expertProposal.ExpertId;
            var existingComment = await _commentService.GetByProposalIdAsync(expertProposalId, cancellationToken);
            bool isSuccess;

            if (existingComment != null)
            {
                existingComment.Text = text;
                existingComment.Rating = rating;
                existingComment.IsApproved = false;
                existingComment.ExpertId = expertId;

                isSuccess = await _commentService.UpdateCommentAsync(existingComment, cancellationToken);
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

                isSuccess = await _commentService.AddCommentAsync(newComment, cancellationToken);
            }

            if (!isSuccess)
            {
                result.Succeeded = false;
                result.Message = "خطا در ثبت یا ویرایش کامنت.";
                return result;
            }

            var allCommentsForExpert = await _commentService.GetCommentsByExpertIdAsync(expertId, cancellationToken);
            if (allCommentsForExpert != null && allCommentsForExpert.Any())
            {
                double newAverageRating = allCommentsForExpert.Average(c => c.Rating);

                var expert = await _userService.GetExpertByIdAsync(expertId, cancellationToken);
                if (expert != null)
                {
                    expert.Rating = newAverageRating;
                    await _userService.UpdateExpertAsync(expert, cancellationToken);
                }
            }

            result.Succeeded = true;
            result.Message = "کامنت شما با موفقیت ثبت شد.";
            return result;
        }


    }
}
