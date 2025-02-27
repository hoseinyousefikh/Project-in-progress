using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.ListOrder
{
    public interface IExpertProposalRepository
    {
        Task<List<ExpertProposal>> GetAllAsync(CancellationToken cancellationToken);
        Task<ExpertProposal> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(ExpertProposal expertProposal, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(ExpertProposal expertProposal, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
