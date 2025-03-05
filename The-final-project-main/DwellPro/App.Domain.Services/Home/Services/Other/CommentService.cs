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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<bool> AddCommentAsync(Comments comment, CancellationToken cancellationToken)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            comment.CreatedAt = DateTime.UtcNow;
            comment.IsApproved = false; 
            return await _commentRepository.AddAsync(comment, cancellationToken);
        }

        public async Task<bool> UpdateCommentAsync(Comments comment, CancellationToken cancellationToken)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            return await _commentRepository.UpdateAsync(comment, cancellationToken);
        }
        public async Task<Comments?> GetByProposalIdAsync(int expertProposalId, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAllAsync(cancellationToken);
            return comments.FirstOrDefault(c => c.ExpertProposalId == expertProposalId);
        }
        public async Task<List<Comments>> GetCommentsByExpertIdAsync(int expertId, CancellationToken cancellationToken)
        {
            if (expertId <= 0)
            {
                throw new ArgumentException("شناسه اکسپرت معتبر نیست.", nameof(expertId));
            }

            var comments = await _commentRepository.GetAllAsync(cancellationToken);

            return comments
                .Where(c => c.ExpertId == expertId)
                .ToList();
        }

    }
}
