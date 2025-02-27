using App.Domain.Core.Home.Contract.Repositories.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder.Order;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.ListOrder.Order
{
    public class ViewOrderService : IViewOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IExpertProposalRepository _expertProposalRepository;
        private readonly IAdminCommentService _commentService;
        public ViewOrderService(IOrderRepository orderRepository, IExpertProposalRepository expertProposalRepository, IAdminCommentService commentService)
        {
            _orderRepository = orderRepository;
            _expertProposalRepository = expertProposalRepository;
            _commentService = commentService;
        }

        public async Task<List<ExpertProposal>> GetProposalsByOrderIdAsync(int orderId, CancellationToken cancellationToken)
        {
            var proposals = await _expertProposalRepository
                .GetAllAsync(cancellationToken);

            var orderProposals = proposals
                .Where(ep => ep.OrderId == orderId && !ep.IsDeleted)
                .ToList();

            var expertCommentsForHomeService = await _commentService.GetAllCommentsAsync(cancellationToken);

            foreach (var proposal in orderProposals)
            {
                var expertCommentsForThisExpert = expertCommentsForHomeService
                    .Where(c => c.ExpertId == proposal.ExpertId && c.IsApproved)
                    .ToList();

                if (expertCommentsForThisExpert.Any())
                {
                    var averageRating = expertCommentsForThisExpert.Average(c => c.Rating);
                    proposal.Expert.Rating = averageRating;
                }
                else
                {
                    proposal.Expert.Rating = 0; 
                }
            }

            var sortedProposals = orderProposals
                .OrderByDescending(ep => ep.Expert.Rating) 
                .ToList();

            return sortedProposals;
        }


    }
}