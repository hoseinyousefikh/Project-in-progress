using App.Domain.Core.Home.Contract.AppServices.ListOrder.Order;
using App.Domain.Core.Home.Contract.Services.ListOrder.Order;
using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.ListOrder.Order
{
    public class ViewOrderAppService : IViewOrderAppService
    {
        private readonly IViewOrderService _viewOrderService;

        public ViewOrderAppService(IViewOrderService viewOrderService)
        {
            _viewOrderService = viewOrderService;
        }

        public Task<List<ExpertProposal>> GetProposalsByOrderIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return _viewOrderService.GetProposalsByOrderIdAsync(orderId, cancellationToken);
        }
    }
}
