using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.ListOrder
{
    public class ExpertProposalAppService : IExpertProposalAppService
    {
        private readonly IExpertProposalService _expertProposalService;

        public ExpertProposalAppService(IExpertProposalService expertProposalService)
        {
            _expertProposalService = expertProposalService;
        }

        public Task<bool> AddExpertProposalAsync(ExpertProposal expertProposal, CancellationToken cancellationToken)
        {
            return _expertProposalService.AddExpertProposalAsync(expertProposal, cancellationToken);
        }

        public Task<List<ExpertProposal>> GetAllExpertProposalsAsync(CancellationToken cancellationToken)
        {
            return _expertProposalService.GetAllExpertProposalsAsync(cancellationToken);
        }

        public Task<ExpertProposal> GetExpertProposalByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _expertProposalService.GetExpertProposalByIdAsync(id, cancellationToken);
        }

        public Task<bool> UpdateExpertProposalAsync(ExpertProposal expertProposal, CancellationToken cancellationToken)
        {
            return _expertProposalService.UpdateExpertProposalAsync(expertProposal, cancellationToken);
        }

        public Task<bool> DeleteExpertProposalAsync(int id, CancellationToken cancellationToken)
        {
            return _expertProposalService.DeleteExpertProposalAsync(id, cancellationToken);
        }
    }
}
