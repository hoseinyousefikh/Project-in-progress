using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.Other
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(Comments comment, CancellationToken cancellationToken);
        Task<bool> UpdateCommentAsync(Comments comment, CancellationToken cancellationToken);
        Task<Comments?> GetByProposalIdAsync(int expertProposalId, CancellationToken cancellationToken);
        Task<List<Comments>> GetCommentsByExpertIdAsync(int expertId, CancellationToken cancellationToken);
    }
}
