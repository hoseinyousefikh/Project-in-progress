using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.Other
{
    public interface ICommentAppService
    {
        Task<bool> AddCommentAsync(Comments comment, CancellationToken cancellationToken);
        Task<bool> UpdateCommentAsync(Comments comment, CancellationToken cancellationToken);
        Task<Comments?> GetByProposalIdAsync(int expertProposalId, CancellationToken cancellationToken);
        Task<List<Comments>> GetCommentsByExpertIdAsync(int expertId, CancellationToken cancellationToken);
        Task<ResultDto> SubmitCommentAsync(int expertProposalId, string text, double rating, ClaimsPrincipal user, CancellationToken cancellationToken);

    }
}
