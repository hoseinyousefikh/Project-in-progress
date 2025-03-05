using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;

namespace App.Domain.AppServices.Home.AppServices.Other
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;
        public CommentAppService(ICommentService commentService)
        {
            _commentService = commentService;
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
    }
}
