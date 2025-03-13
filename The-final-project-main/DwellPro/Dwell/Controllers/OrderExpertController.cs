using App.Domain.AppServices.Home.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.Categories;
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
        private readonly IHomeServiceAppService _homeServiceAppService;
        public OrderExpertController(IOrderAppService orderAppService, IExpertProposalAppService expertProposalAppService , IHomeServiceAppService homeServiceAppService)
        {
            _orderAppService = orderAppService;
            _expertProposalAppService = expertProposalAppService;
            _homeServiceAppService = homeServiceAppService;
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

        [HttpGet]
        public async Task<IActionResult> ExpertProposalsPending(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("شناسه کاربر معتبر نیست.");
            }

            try
            {
                var expertProposals = await _expertProposalAppService
                    .GetExpertProposalsByExpertIdAsync(userId, cancellationToken);

                var pendingProposals = expertProposals
                    .Where(proposal => proposal.ProposalStatus == ProposalStatus.Pending)
                    .ToList();

                ViewBag.HomeServiceNames = await Task.WhenAll(
                    pendingProposals
                        .Select(async proposal =>
                        {
                            var homeService = await _homeServiceAppService
                                .GetHomeServiceByIdAsync(proposal.Order.HomeServiceId, cancellationToken);
                            return homeService?.Name ?? "نامشخص";
                        })
                );

                if (!pendingProposals.Any())
                {
                    ViewBag.Message = "هیچ پیشنهاد در انتظار تأییدی یافت نشد.";
                }

                return View(pendingProposals);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.Message = ex.Message;
                return View(new List<ExpertProposal>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExpertProposalsAccepted(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("شناسه کاربر معتبر نیست.");
            }

            try
            {
                var expertProposals = await _expertProposalAppService
                    .GetExpertProposalsByExpertIdAsync(userId, cancellationToken);

                var acceptedProposals = expertProposals
                    .Where(proposal => proposal.ProposalStatus == ProposalStatus.Accepted)
                    .ToList();

                var homeServiceNames = new List<string>();
                var paymentStatuses = new List<string>();

                foreach (var proposal in acceptedProposals)
                {
                    var homeService = await _homeServiceAppService
                        .GetHomeServiceByIdAsync(proposal.Order.HomeServiceId, cancellationToken);

                    var order = await _orderAppService
                        .GetOrderByIdAsync(proposal.OrderId, cancellationToken);

                    homeServiceNames.Add(homeService?.Name ?? "نامشخص");
                    paymentStatuses.Add(order?.PaymentStatus.ToString() ?? "وضعیت نامشخص");
                }

                ViewBag.HomeServiceNames = homeServiceNames;
                ViewBag.PaymentStatuses = paymentStatuses;

                return View(acceptedProposals);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.Message = ex.Message;
                return View(new List<ExpertProposal>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExpertProposalsRejected(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("شناسه کاربر معتبر نیست.");
            }

            try
            {
                var expertProposals = await _expertProposalAppService.GetExpertProposalsByExpertIdAsync(userId, cancellationToken);

                var pendingProposals = expertProposals
                    .Where(proposal => proposal.ProposalStatus == ProposalStatus.Rejected)
                    .ToList();
                ViewBag.HomeServiceNames = await Task.WhenAll(
                  pendingProposals
                      .Select(async proposal =>
                      {
                          var homeService = await _homeServiceAppService
                              .GetHomeServiceByIdAsync(proposal.Order.HomeServiceId, cancellationToken);
                          return homeService?.Name ?? "نامشخص";
                      })
              );

               

                return View(pendingProposals);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.Message = ex.Message;
                return View(new List<ExpertProposal>());
            }
        }
        public IActionResult ProposalStatusOptions()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailsForExpert(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.GetOrderByIdAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
