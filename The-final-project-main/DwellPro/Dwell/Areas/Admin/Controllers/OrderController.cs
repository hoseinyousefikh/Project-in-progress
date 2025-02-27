using App.Domain.Core.Home.Contract.AppServices.ListOrder.Order;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderManagementAppService _orderManagementAppService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderManagementAppService orderManagementAppService, ILogger<OrderController> logger)
        {
            _orderManagementAppService = orderManagementAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var orders = await _orderManagementAppService.GetAllOrdersAsync(cancellationToken);

            ViewData["OrderStatuses"] = Enum.GetValues(typeof(OrderStatus));
            ViewData["PaymentStatuses"] = Enum.GetValues(typeof(PaymentStatus));
            _logger.LogInformation("اردر استاتوس و پیمنت استاتوس به ویو اضافه شد");
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int orderId, OrderStatus newStatus, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderManagementAppService.GetOrderByIdAsync(orderId, cancellationToken);

                if (newStatus == OrderStatus.Completed && order.PaymentStatus != PaymentStatus.Paid)
                {
                    _logger.LogWarning("برای تغییر وضعیت به اتمام کار، باید پرداخت انجام شده باشد");
                    TempData["ErrorMessage"] = "برای تغییر وضعیت به اتمام کار، باید پرداخت انجام شده باشد.";
                    return RedirectToAction("Index");
                }

                await _orderManagementAppService.UpdateOrderStatusAsync(orderId, newStatus, cancellationToken);
                _logger.LogInformation("ضعیت سفارش با موفقیت تغییر کرد");
                TempData["SuccessMessage"] = "وضعیت سفارش با موفقیت تغییر کرد!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> ChangePaymentStatus(int orderId, PaymentStatus newPaymentStatus, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderManagementAppService.GetOrderByIdAsync(orderId, cancellationToken);

                if (order.OrderStatus != OrderStatus.WaitingForExpertArrival)
                {
                    _logger.LogWarning("برای تغییر وضعیت پرداخت، وضعیت سفارش باید 'منتظر آمدن متخصص به محل' باشد.");
                    TempData["ErrorMessage"] = "برای تغییر وضعیت پرداخت، وضعیت سفارش باید 'منتظر آمدن متخصص به محل' باشد.";
                    return RedirectToAction("Index");
                }

                await _orderManagementAppService.UpdatePaymentStatusAsync(orderId, newPaymentStatus, cancellationToken);
                _logger.LogInformation("وضعیت پرداخت سفارش با موفقیت تغییر کرد");
                TempData["SuccessMessage"] = "وضعیت پرداخت سفارش با موفقیت تغییر کرد!";
            }
            catch (Exception ex)
            {
                _logger.LogError("وضعیت پرداخت با مسل مواجه شد");
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}
