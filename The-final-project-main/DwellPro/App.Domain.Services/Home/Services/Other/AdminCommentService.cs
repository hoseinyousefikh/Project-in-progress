using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.Other
{
    public class AdminCommentService : IAdminCommentService
    {
        private readonly ICommentRepository _commentRepository;

        public AdminCommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<Comments>> GetAllCommentsAsync(CancellationToken cancellationToken)
        {
            return await _commentRepository.GetAllAsync(cancellationToken);
        }

        public async Task<bool> ApproveCommentAsync(int id, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);
            if (comment != null)
            {
                comment.IsApproved = true;
                return await _commentRepository.UpdateAsync(comment, cancellationToken);
            }
            return false;
        }

        public async Task<bool> RejectCommentAsync(int id, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);
            if (comment != null)
            {
                comment.IsApproved = false;
                return await _commentRepository.UpdateAsync(comment, cancellationToken);
            }
            return false;
        }

        public async Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken)
        {
            return await _commentRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<Comments> GetCommentDetailsAsync(int id, CancellationToken cancellationToken)
        {
            return await _commentRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}
