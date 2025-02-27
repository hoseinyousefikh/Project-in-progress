using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.ListOrder
{
    public interface IExpertProposalService
    {
        Task<bool> AddExpertProposalAsync(ExpertProposal expertProposal, CancellationToken cancellationToken);
        Task<List<ExpertProposal>> GetAllExpertProposalsAsync(CancellationToken cancellationToken);
        Task<ExpertProposal> GetExpertProposalByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateExpertProposalAsync(ExpertProposal expertProposal, CancellationToken cancellationToken);
        Task<bool> DeleteExpertProposalAsync(int id, CancellationToken cancellationToken);
    }
}
