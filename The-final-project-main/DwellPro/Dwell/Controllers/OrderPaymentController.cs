using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.ListOrder.Order;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using App.Domain.Services.Home.Services.Users;
using DwellMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DwellMVC.Controllers
{
    public class OrderPaymentController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly IUserAppService _userAppService;
        private readonly IViewOrderAppService _viewOrderAppService;


        public OrderPaymentController(IOrderAppService orderAppService, IAdminUserAppService adminUserAppService, IUserAppService userAppService, IViewOrderAppService viewOrderAppService)
        {
            _orderAppService = orderAppService;
            _adminUserAppService = adminUserAppService;
            _userAppService = userAppService;
            _viewOrderAppService = viewOrderAppService;
        }
        public async Task<IActionResult> GetAllOrderByStatus(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
            {
                return RedirectToAction("Login", "Authentication");
            }

            var user = await _adminUserAppService.GetByIdAsync(parsedUserId, cancellationToken);
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var customer = user?.CustomerDetails;
            if (customer == null || customer.UserId != parsedUserId)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var orders = await _orderAppService.GetOrdersByStatusAndCustomerIdAsync(OrderStatus.WaitingForExpertArrival, customer.Id, cancellationToken);

            var orderViewModels = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                ServiceTitle = o.HomeServiceName?.Name,
                Price = o.BasePrice ?? 0,
                ImageUrl = o.HomeServiceName?.ImageUrl,
                Status = o.OrderStatus.ToString(),
                OrderDate = o.RequestDate
            }).ToList();

            ViewBag.Orders = orderViewModels;

            return View(orderViewModels);
        }


        [HttpGet]
        public IActionResult CompleteOrder()
        {
            return  View();
        }


        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.GetOrderByIdAsync(orderId, cancellationToken);
            if (order == null || order.OrderStatus != OrderStatus.WaitingForExpertArrival)
            {
                return BadRequest("سفارش معتبر نیست یا در وضعیت درستی قرار ندارد.");
            }

            var Result = await _userAppService.GetCustomerByIdAsync(order.CustomerId, cancellationToken);
            var userCustomer = await _adminUserAppService.GetByIdAsync(Result.UserId, cancellationToken);
            if (userCustomer == null)
            {
                return BadRequest("مشتری مرتبط با این سفارش یافت نشد.");
            }

            var proposals = await _viewOrderAppService.GetProposalsByOrderIdAsync(orderId, cancellationToken);
            var approvedProposal = proposals.FirstOrDefault(p => p.IsSelectedByCustomer);
            if (approvedProposal == null)
            {
                return BadRequest("هیچ پیشنهادی تایید نشده است.");
            }

            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);
            var expert = experts.FirstOrDefault(e => e.Id == approvedProposal.ExpertId);
            if (expert == null)
            {
                return BadRequest("اکسپرت مرتبط با پیشنهاد تایید شده یافت نشد.");
            }

            var expertUser = await _adminUserAppService.GetByIdAsync(expert.User.Id, cancellationToken);
            if (expertUser == null)
            {
                return BadRequest("کاربر اکسپرت پیدا نشد.");
            }

            decimal transactionAmount = approvedProposal.ProposedPrice;
            decimal platformFee = transactionAmount * 0.05m; 
            decimal customerAmount = transactionAmount + (transactionAmount * 0.025m);
            decimal expertAmount = transactionAmount - (transactionAmount * 0.025m);

            userCustomer.Balance -= customerAmount;
            if (userCustomer.Balance < 0)
            {
                return BadRequest("موجودی کافی برای انجام تراکنش وجود ندارد.");
            }
            await _userAppService.UpdateUser(userCustomer, cancellationToken);

            expertUser.Balance += expertAmount;
            await _userAppService.UpdateUser(expertUser, cancellationToken);

            var adminUser = await _adminUserAppService.GetByIdAsync(1, cancellationToken); 
            if (adminUser != null)
            {
                adminUser.Balance += platformFee;
                await _userAppService.UpdateUser(adminUser, cancellationToken);
            }

            order.OrderStatus = OrderStatus.Completed;
            order.PaymentStatus = PaymentStatus.Paid;
            await _orderAppService.UpdateOrderAsync(order, cancellationToken);

            return RedirectToAction("CompleteOrder", "OrderPayment");
        }
        //

    }
}
