using App.Domain.Core.Home.Contract.AppServices.ListOrder.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DwellMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderProposalsController : Controller
    {
        private readonly IViewOrderAppService _viewOrderAppService;
        private readonly IOrderManagementAppService _orderManagementAppService;

        public OrderProposalsController(IViewOrderAppService viewOrderAppService, IOrderManagementAppService orderManagementAppService)
        {
            _viewOrderAppService = viewOrderAppService;
            _orderManagementAppService = orderManagementAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderManagementAppService.GetAllOrdersAsync(cancellationToken);
                return View(orders);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Proposals(int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var proposals = await _viewOrderAppService.GetProposalsByOrderIdAsync(orderId, cancellationToken);

                return View(proposals);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
