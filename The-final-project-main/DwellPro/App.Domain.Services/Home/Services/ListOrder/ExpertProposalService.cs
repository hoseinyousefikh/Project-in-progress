using App.Domain.Core.Home.Contract.Repositories.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.ListOrder
{
    public class ExpertProposalService : IExpertProposalService
    {
        private readonly IExpertProposalRepository _expertProposalRepository;

        public ExpertProposalService(IExpertProposalRepository expertProposalRepository)
        {
            _expertProposalRepository = expertProposalRepository;
        }

        public async Task<bool> AddExpertProposalAsync(ExpertProposal expertProposal, CancellationToken cancellationToken)
        {
            if (expertProposal == null)
                throw new ArgumentNullException(nameof(expertProposal));

            return await _expertProposalRepository.AddAsync(expertProposal, cancellationToken);
        }

        public async Task<List<ExpertProposal>> GetAllExpertProposalsAsync(CancellationToken cancellationToken)
        {
            return await _expertProposalRepository.GetAllAsync(cancellationToken);
        }

        public async Task<ExpertProposal> GetExpertProposalByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _expertProposalRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateExpertProposalAsync(ExpertProposal expertProposal, CancellationToken cancellationToken)
        {
            if (expertProposal == null)
                throw new ArgumentNullException(nameof(expertProposal));

            return await _expertProposalRepository.UpdateAsync(expertProposal, cancellationToken);
        }

        public async Task<bool> DeleteExpertProposalAsync(int id, CancellationToken cancellationToken)
        {
            return await _expertProposalRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
