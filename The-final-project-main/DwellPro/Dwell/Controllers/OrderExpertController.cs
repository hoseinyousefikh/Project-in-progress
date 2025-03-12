using App.Domain.AppServices.Home.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DwellMVC.Controllers
{
    public class OrderExpertController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IExpertProposalAppService _expertProposalAppService;
        public OrderExpertController(IOrderAppService orderAppService, IExpertProposalAppService expertProposalAppService)
        {
            _orderAppService = orderAppService;
            _expertProposalAppService = expertProposalAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredOrdersForExpert(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                ViewBag.ErrorMessage = "شناسه کاربر معتبر نیست.";
                return RedirectToAction("Login", "Authentication");
            }

            var result = await _orderAppService.GetFilteredOrdersForExpertAsync(userId, cancellationToken);

            if (!result.Succeeded)
            {
                ViewBag.ErrorMessage = result.Message;
                return View("Error");
            }

            return View("GetFilteredOrdersForExpert", result.Orders);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProposal(int orderId, CancellationToken cancellationToken)
        {
            var allOrders = await _orderAppService.GetAllOrdersAsync(cancellationToken);
            var order = allOrders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                TempData["ErrorMessage"] = "سفارش یافت نشد.";
                return RedirectToAction("GetFilteredOrdersForExpert", "OrderExpert");
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Authentication");
            }

            var isProposalSubmitted = await _expertProposalAppService.IsProposalSubmittedForOrderAsync(orderId, userId, cancellationToken);
            OrderDto orderDto = new OrderDto();
            if (isProposalSubmitted)
            {
                orderDto.IsProposalSubmitted = false;
                TempData["ErrorMessage"] = "شما قبلاً برای این سفارش پیشنهاد ثبت کرده‌اید.";
                return RedirectToAction("GetFilteredOrdersForExpert", "OrderExpert");
            }

            var proposalModel = new ExpertProposalDto
            {
                OrderId = order.Id,
                ServiceTitle = order.HomeServiceName?.Name,
                OrderDate = order.RequestDate,
                WorkCompletionDate = DateTime.Now.AddDays(7),
                ProposalDate = DateTime.Now,
                ProposedExecutionTime = TimeSpan.FromHours(5),
            };

            return View(proposalModel);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitProposal(ExpertProposalDto model, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Authentication");
            }

            model.ExpertId = userId;
            model.ProposalDate = DateTime.Now;
            var result = await _expertProposalAppService.SubmitProposalAsync(model, cancellationToken);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("CreateProposal", new { orderId = model.OrderId });
            }

            TempData["SuccessMessage"] = "پیشنهاد شما با موفقیت ثبت شد.";
            return RedirectToAction("GetFilteredOrdersForExpert", "OrderExpert");
        }


    }
}
