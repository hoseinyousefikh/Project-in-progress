using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.ListOrder.Order
{
    public interface IViewOrderService
    {
        Task<List<ExpertProposal>> GetProposalsByOrderIdAsync(int orderId, CancellationToken cancellationToken);
    }
}
