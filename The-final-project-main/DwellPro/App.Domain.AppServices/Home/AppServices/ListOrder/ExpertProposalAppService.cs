using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Contract.Services.Users;
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
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly IAdminUserService _adminUserService;
        public ExpertProposalAppService(IExpertProposalService expertProposalService, IOrderService orderService, IAdminUserAppService adminUserAppService, IAdminUserService adminUserService)
        {
            _expertProposalService = expertProposalService;
            _orderService = orderService;
            _adminUserAppService = adminUserAppService;
            _adminUserService = adminUserService;
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
            proposal.CustomerSelectionDate = DateTime.Now;


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
        public async Task<ResultDto> SubmitProposalAsync(ExpertProposalDto model, CancellationToken cancellationToken)
        {
            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);
            var currentUserExpert = experts.FirstOrDefault(expert => expert.User.Id == model.ExpertId);

            if (currentUserExpert == null)
            {
                return new ResultDto
                {
                    Succeeded = false,
                    Message = "اکسپرت مربوط به کاربر پیدا نشد."
                };
            }

            var user = await _adminUserService.GetByIdAsync(currentUserExpert.Id, cancellationToken);
            if (user == null || user.ExpertDetails == null || user.ExpertDetails.UserId != user.Id)
            {
                return new ResultDto
                {
                    Succeeded = false,
                    Message = "کاربر یافت نشد یا معتبر نیست."
                };
            }

            var expertProposal = new ExpertProposal
            {
                ProposedPrice = model.ProposedPrice,
                ProposalDescription = model.ProposalDescription,
                ProposalDate = model.ProposalDate,
                ProposedExecutionTime = model.ProposedExecutionTime,
                WorkCompletionDate = model.WorkCompletionDate,
                CustomerSelectionDate = model.CustomerSelectionDate,
                OrderId = model.OrderId,
                ExpertId = currentUserExpert.Id, 
                ProposalStatus = ProposalStatus.Pending, 
                IsDeleted = false,
                IsApproved = false,
                IsSelectedByCustomer = false
            };

            var result = await _expertProposalService.AddExpertProposalAsync(expertProposal, cancellationToken);

            if (!result)
            {
                return new ResultDto
                {
                    Succeeded = false,
                    Message = "ثبت پیشنهاد با خطا مواجه شد."
                };
            }

            return new ResultDto
            {
                Succeeded = true,
                Message = "پیشنهاد شما با موفقیت ثبت شد."
            };
        }
        public async Task<bool> IsProposalSubmittedForOrderAsync(int orderId, int userId, CancellationToken cancellationToken)
        {
            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);

            var currentUserExpert = experts.FirstOrDefault(expert => expert.User.Id == userId);

            if (currentUserExpert == null)
            {
                throw new InvalidOperationException("اکسپرت مربوط به کاربر پیدا نشد.");
            }

            var allProposals = await _expertProposalService.GetAllExpertProposalsAsync(cancellationToken);
            var proposalsForOrder = allProposals.Where(p => p.OrderId == orderId && p.ExpertId == currentUserExpert.Id && p.IsDeleted == false).ToList();

            var isProposalSubmitted = proposalsForOrder.Any();

            return isProposalSubmitted;
        }

    }
}
