using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.ListOrder.Order
{
    public interface IViewOrderAppService
    {
        Task<List<ExpertProposal>> GetProposalsByOrderIdAsync(int orderId, CancellationToken cancellationToken);
    }
}
