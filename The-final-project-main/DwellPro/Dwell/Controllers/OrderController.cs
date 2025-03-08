using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using DwellMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DwellMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly IExpertProposalAppService _expertProposalAppService;
        private readonly IUserAppService _userAppService;
        private readonly IPictureAppService _pictureAppService;
        public OrderController(IOrderAppService orderAppService, IHomeServiceAppService homeServiceAppService, IAdminUserAppService adminUserAppService, IExpertProposalAppService expertProposalAppService, IUserAppService userAppService , IPictureAppService pictureAppService)
        {
            _orderAppService = orderAppService;
            _homeServiceAppService = homeServiceAppService;
            _adminUserAppService = adminUserAppService;
            _expertProposalAppService = expertProposalAppService;
            _userAppService = userAppService;
            _pictureAppService = pictureAppService;
        }

        //[HttpGet]
        //public async Task<IActionResult> CreateOrder(int homeServiceId, CancellationToken cancellationToken)
        //{
        //    var homeService = await _homeServiceAppService.GetHomeServiceByIdAsync(homeServiceId, cancellationToken);

        //    if (homeService == null)
        //    {
        //        return NotFound();
        //    }

        //    homeService.ViewCount += 1;
        //    await _homeServiceAppService.UpdateHomeServiceAsync(homeService, cancellationToken);

        //    var users = await _adminUserAppService.GetAllAsync(cancellationToken);
        //    var customers = users.OfType<Customers>().ToList();

        //    var model = new CreateOrderViewModel
        //    {
        //        HomeServiceId = homeService.Id,
        //        HomeServices = new List<HomeService> { homeService },
        //        Users = customers,
        //        SelectedHomeService = homeService,
        //        BasePrice = homeService.BasePrice
        //    };

        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> CreateOrder(int homeServiceId, CancellationToken cancellationToken)
        {
            var orderData = await _orderAppService.GetCreateOrderDataAsync(homeServiceId, cancellationToken);

            if (orderData == null)
            {
                return NotFound();
            }

            var model = new CreateOrderViewModel
            {
                HomeServiceId = orderData.HomeServiceId,
                HomeServices = orderData.HomeServices,
                Users = orderData.Users,
                SelectedHomeService = orderData.SelectedHomeService,
                BasePrice = orderData.BasePrice
            };

            return View(model);
        }


        //[HttpPost]
        //public async Task<IActionResult> CreateOrder(CreateOrderViewModel model, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        //    {
        //        ModelState.AddModelError("", "شناسه کاربر معتبر نیست.");
        //        return View(model);
        //    }

        //    var user = await _adminUserAppService.GetByIdAsync(userId, cancellationToken);
        //    var customer = user?.CustomerDetails;

        //    if (customer == null)
        //    {
        //        ModelState.AddModelError("", "کاستومر مرتبط با این کاربر پیدا نشد.");
        //        return View(model);
        //    }

        //    var order = new Orders
        //    {
        //        RequestDate = DateTime.Now,
        //        ExecutionDate = model.ExecutionDate,
        //        ExecutionTime = model.ExecutionTime,
        //        Description = model.Description,
        //        BasePrice = model.BasePrice,
        //        OrderStatus = OrderStatus.WaitingForExpertProposal,
        //        PaymentStatus = PaymentStatus.Pending,
        //        IsApproved = false,
        //        IsDeleted = false,
        //        HomeServiceId = model.HomeServiceId,
        //        CustomerId = customer.Id
        //    };

        //    var result = await _orderAppService.AddOrderAsync(order, cancellationToken);
        //    if (!result)
        //    {
        //        ModelState.AddModelError("", "خطا در ثبت سفارش. لطفاً دوباره تلاش کنید.");
        //        return View(model);
        //    }

        //    if (model.Pictures != null && model.Pictures.Any())
        //    {
        //        var pictureTasks = model.Pictures.Select(async pictureFile =>
        //        {
        //            var imageUrl = await _pictureAppService.SaveImageAsync(pictureFile, cancellationToken);
        //            return new Pictures
        //            {
        //                OrdersId = order.Id,
        //                ImageUrl = imageUrl,
        //                UploadedAt = DateTime.UtcNow
        //            };
        //        });

        //        var pictures = await Task.WhenAll(pictureTasks);
        //        foreach (var picture in pictures)
        //        {
        //            await _pictureAppService.AddPictureAsync(picture, cancellationToken);
        //        }
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var orderDto = new CreateOrderDto
            {
                HomeServiceId = model.HomeServiceId,
                ExecutionDate = model.ExecutionDate,
                ExecutionTime = model.ExecutionTime,
                Description = model.Description,
                BasePrice = model.BasePrice,
                Pictures = model.Pictures
            };

            var result = await _orderAppService.CreateOrderAsync(orderDto, User, cancellationToken);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }


        //[HttpGet]
        //public async Task<IActionResult> GetOrdersForUser(CancellationToken cancellationToken)
        //{
        //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (userIdClaim == null)
        //    {
        //        ModelState.AddModelError("", "کاربر شناسایی نشد.");
        //        return View("Error");
        //    }

        //    if (int.TryParse(userIdClaim, out int userId))
        //    {
        //        var user = await _adminUserAppService.GetByIdAsync(userId, cancellationToken);
        //        Customers customer = user?.CustomerDetails;

        //        if (customer == null)
        //        {
        //            ModelState.AddModelError("", "کاستومر مرتبط با این کاربر پیدا نشد.");
        //            return View("Error");
        //        }

        //        var allOrders = await _orderAppService.GetAllOrdersAsync(cancellationToken);
        //        var userOrders = allOrders.Where(order => order.CustomerId == customer.Id&& order.IsDeleted == false ).ToList();

        //        return View(userOrders);
        //    }

        //    ModelState.AddModelError("", "شناسه کاربر معتبر نیست.");
        //    return View("Error");
        //}

        [HttpGet]
        public async Task<IActionResult> GetOrdersForUser(CancellationToken cancellationToken)
        {
            var userOrders = await _orderAppService.GetOrdersForUserAsync(User, cancellationToken);

            return View(userOrders ?? new List<Orders>());
        }

        [HttpGet]
        public async Task<IActionResult> Proposals(int orderId, CancellationToken cancellationToken)
        {
            var model = await _orderAppService.GetProposalsForOrderAsync(orderId, cancellationToken);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        //[HttpGet]
        //public async Task<IActionResult> Proposals(int orderId, CancellationToken cancellationToken)
        //{
        //    var order = await _orderAppService.GetOrderByIdAsync(orderId, cancellationToken);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    var proposals = order.ExpertProposals;

        //    var model = new ProposalViewModel
        //    {
        //        OrderId = orderId,
        //        Proposals = new List<ProposalViewItem>()
        //    };

        //    foreach (var proposal in proposals)
        //    {
        //        try
        //        {
        //            var expert = await _userAppService.GetExpertByIdAsync(proposal.ExpertId, cancellationToken);
        //            model.Proposals.Add(new ProposalViewItem
        //            {
        //                Proposal = proposal,
        //                Expert = expert
        //            });
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }

        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.GetOrderByIdAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteConfirm(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.GetOrderByIdAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _orderAppService.DeleteOrderAsync(id, cancellationToken);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("GetOrdersForUser");
        }

        //[HttpPost]
        //public async Task<IActionResult> AcceptProposal(int proposalId, CancellationToken cancellationToken)
        //{
        //    var proposal = await _expertProposalAppService.GetExpertProposalByIdAsync(proposalId, cancellationToken);

        //    if (proposal == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = proposal.Order;
        //    var existingAcceptedProposal = order.ExpertProposals.FirstOrDefault(p => p.IsSelectedByCustomer);

        //    if (existingAcceptedProposal != null && existingAcceptedProposal.Id != proposalId)
        //    {
        //        TempData["ErrorMessage"] = "یک پیشنهاد قبلاً تأیید شده است و امکان تغییر وجود ندارد.";
        //        return RedirectToAction("Proposals", new { orderId = order.Id });
        //    }

        //    proposal.IsSelectedByCustomer = true;

        //    foreach (var otherProposal in order.ExpertProposals.Where(p => p.Id != proposalId))
        //    {
        //        otherProposal.IsSelectedByCustomer = false;
        //    }

        //    order.OrderStatus = OrderStatus.WaitingForExpertArrival;

        //    foreach (var updatedProposal in order.ExpertProposals)
        //    {
        //        var updateResult = await _expertProposalAppService.UpdateExpertProposalAsync(updatedProposal, cancellationToken);
        //        if (!updateResult)
        //        {
        //            TempData["ErrorMessage"] = "خطا در به‌روزرسانی پیشنهادات.";
        //            return RedirectToAction("Proposals", new { orderId = order.Id });
        //        }
        //    }

        //    var orderUpdateResult = await _orderAppService.UpdateOrderAsync(order, cancellationToken);
        //    if (!orderUpdateResult)
        //    {
        //        TempData["ErrorMessage"] = "خطا در به‌روزرسانی وضعیت سفارش.";
        //        return RedirectToAction("Proposals", new { orderId = order.Id });
        //    }

        //    return RedirectToAction("Proposals", new { orderId = order.Id });
        //}
        [HttpPost]
        public async Task<IActionResult> AcceptProposal(int proposalId, CancellationToken cancellationToken)
        {
            var result = await _expertProposalAppService.AcceptProposalAsync(proposalId, cancellationToken);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            else
            {
                TempData["SuccessMessage"] = result.Message;
            }

            return RedirectToAction("Proposals", new { orderId = result.OrderId });
        }
 

    }
}
