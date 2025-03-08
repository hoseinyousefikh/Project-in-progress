using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
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
        private readonly IOrderService _orderService;

        public ExpertProposalAppService(IExpertProposalService expertProposalService, IOrderService orderService)
        {
            _expertProposalService = expertProposalService;
            _orderService = orderService;

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
        public async Task<ResultDto> AcceptProposalAsync(int proposalId, CancellationToken cancellationToken)
        {
            var proposal = await _expertProposalService.GetExpertProposalByIdAsync(proposalId, cancellationToken);
            if (proposal == null)
            {
                return new ResultDto { Succeeded = false, Message = "پیشنهاد مورد نظر یافت نشد." };
            }

            var order = proposal.Order;
            var existingAcceptedProposal = order.ExpertProposals.FirstOrDefault(p => p.IsSelectedByCustomer);

            if (existingAcceptedProposal != null && existingAcceptedProposal.Id != proposalId)
            {
                return new ResultDto { Succeeded = false, Message = "یک پیشنهاد قبلاً تأیید شده است و امکان تغییر وجود ندارد.", OrderId = order.Id };
            }

            proposal.IsSelectedByCustomer = true;

            foreach (var otherProposal in order.ExpertProposals.Where(p => p.Id != proposalId))
            {
                otherProposal.IsSelectedByCustomer = false;
            }

            order.OrderStatus = OrderStatus.WaitingForExpertArrival;

            foreach (var updatedProposal in order.ExpertProposals)
            {
                var updateResult = await _expertProposalService.UpdateExpertProposalAsync(updatedProposal, cancellationToken);
                if (!updateResult)
                {
                    return new ResultDto { Succeeded = false, Message = "خطا در به‌روزرسانی پیشنهادات.", OrderId = order.Id };
                }
            }

            var orderUpdateResult = await _orderService.UpdateOrderAsync(order, cancellationToken);
            if (!orderUpdateResult)
            {
                return new ResultDto { Succeeded = false, Message = "خطا در به‌روزرسانی وضعیت سفارش.", OrderId = order.Id };
            }

            return new ResultDto { Succeeded = true, Message = "پیشنهاد با موفقیت پذیرفته شد.", OrderId = order.Id };
        }

    }
}
