using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder.Order;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.ListOrder
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        private readonly IHomeServiceService _homeServiceService;
        private readonly IAdminUserService _adminUserService;
        private readonly IPictureService _pictureService;
        private readonly IPictureAppService _pictureAppService;
        private readonly IUserService _userService;
        private readonly IViewOrderService _viewOrderService;
        private readonly IAdminUserAppService _adminUserAppService;

        public OrderAppService(IOrderService orderService, IHomeServiceService homeServiceService, IAdminUserService adminUserService, IPictureService pictureService, IPictureAppService pictureAppService, IUserService userService, IViewOrderService viewOrderService, IAdminUserAppService adminUserAppService)
        {
            _orderService = orderService;
            _homeServiceService = homeServiceService;
            _adminUserService = adminUserService;
            _pictureService = pictureService;
            _pictureAppService = pictureAppService;
            _userService = userService;
            _viewOrderService = viewOrderService;
            _adminUserAppService = adminUserAppService;
        }

        public Task<bool> AddOrderAsync(Orders order, CancellationToken cancellationToken)
        {
            return _orderService.AddOrderAsync(order, cancellationToken);
        }

        public Task<List<Orders>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return _orderService.GetAllOrdersAsync(cancellationToken);
        }

        public Task<Orders> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _orderService.GetOrderByIdAsync(id, cancellationToken);
        }

        public Task<bool> UpdateOrderAsync(Orders order, CancellationToken cancellationToken)
        {
            return _orderService.UpdateOrderAsync(order, cancellationToken);
        }

        public Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken)
        {
            return _orderService.DeleteOrderAsync(id, cancellationToken);
        }
        public async Task<List<Orders>> GetOrdersByStatusAndCustomerIdAsync(OrderStatus status, int customerId, CancellationToken cancellationToken)
        {
            var allOrders = await _orderService.GetAllOrdersAsync(cancellationToken);

            return allOrders
                .Where(o => o.OrderStatus == status && o.CustomerId == customerId)
                .ToList();
        }

        public async Task<CreateOrderDto> GetCreateOrderDataAsync(int homeServiceId, CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceService.GetHomeServiceByIdAsync(homeServiceId, cancellationToken);
            if (homeService == null)
            {
                return null;
            }

            homeService.ViewCount += 1;
            await _homeServiceService.UpdateHomeServiceAsync(homeService, cancellationToken);

            var users = await _adminUserService.GetAllAsync(cancellationToken);
            var customers = users.OfType<Customers>().ToList();

            return new CreateOrderDto
            {
                HomeServiceId = homeService.Id,
                HomeServices = new List<HomeService> { homeService },
                Users = customers,
                SelectedHomeService = homeService,
                BasePrice = homeService.BasePrice
            };
        }
        public async Task<ResultDto> CreateOrderAsync(CreateOrderDto orderDto, ClaimsPrincipal user, CancellationToken cancellationToken)
        {
            if (!int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return new ResultDto { Succeeded = false, Message = "شناسه کاربر معتبر نیست." };
            }

            var userInfo = await _adminUserService.GetByIdAsync(userId, cancellationToken);
            var customer = userInfo?.CustomerDetails;

            if (customer == null)
            {
                return new ResultDto { Succeeded = false, Message = "کاستومر مرتبط با این کاربر پیدا نشد." };
            }

            var order = new Orders
            {
                RequestDate = DateTime.Now,
                ExecutionDate = orderDto.ExecutionDate,
                ExecutionTime = orderDto.ExecutionTime,
                Description = orderDto.Description,
                BasePrice = orderDto.BasePrice,
                OrderStatus = OrderStatus.WaitingForExpertProposal,
                PaymentStatus = PaymentStatus.Pending,
                IsApproved = false,
                IsDeleted = false,
                HomeServiceId = orderDto.HomeServiceId,
                CustomerId = customer.Id
            };

            var result = await _orderService.AddOrderAsync(order, cancellationToken);
            if (!result)
            {
                return new ResultDto { Succeeded = false, Message = "خطا در ثبت سفارش. لطفاً دوباره تلاش کنید." };
            }

            if (orderDto.Pictures != null && orderDto.Pictures.Any())
            {
                var pictureTasks = orderDto.Pictures.Select(async pictureFile =>
                {
                    var imageUrl = await _pictureAppService.SaveImageAsync(pictureFile, cancellationToken);
                    return new Pictures
                    {
                        OrdersId = order.Id,
                        ImageUrl = imageUrl,
                        UploadedAt = DateTime.UtcNow
                    };
                });

                var pictures = await Task.WhenAll(pictureTasks);
                foreach (var picture in pictures)
                {
                    await _pictureService.AddPictureAsync(picture, cancellationToken);
                }
            }

            return new ResultDto { Succeeded = true, Message = "سفارش با موفقیت ثبت شد." };
        }
        public async Task<List<Orders>> GetOrdersForUserAsync(ClaimsPrincipal user, CancellationToken cancellationToken)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new List<Orders>(); 
            }

            var userInfo = await _adminUserService.GetByIdAsync(userId, cancellationToken);
            var customer = userInfo?.CustomerDetails;

            if (customer == null)
            {
                return new List<Orders>();
            }

            var allOrders = await _orderService.GetAllOrdersAsync(cancellationToken);
            var userOrders = allOrders.Where(order => order.CustomerId == customer.Id && !order.IsDeleted).ToList();

            return userOrders;
        }
        public async Task<ProposalDto> GetProposalsForOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await GetOrderByIdAsync(orderId, cancellationToken);

            if (order == null)
            {
                return null;
            }

            var proposals = order.ExpertProposals;

            var model = new ProposalDto
            {
                OrderId = orderId,
                Proposals = new List<ProposalViewItem>()
            };

            foreach (var proposal in proposals)
            {
                try
                {
                    var expert = await _userService.GetExpertByIdAsync(proposal.ExpertId, cancellationToken);
                    model.Proposals.Add(new ProposalViewItem
                    {
                        Proposal = proposal,
                        Expert = expert
                    });
                }
                catch (Exception)
                {
                }
            }

            return model;
        }
        public async Task<OrderResultDto> GetAllOrderByStatusAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _adminUserService.GetByIdAsync(userId, cancellationToken);
            if (user == null || user.CustomerDetails == null || user.CustomerDetails.UserId != userId)
            {
                return new OrderResultDto { Succeeded = false, Message = "کاربر یافت نشد یا معتبر نیست.", Orders = null };
            }

            var customer = user.CustomerDetails;
            var orders = await GetOrdersByStatusAndCustomerIdAsync(OrderStatus.WaitingForExpertArrival, customer.Id, cancellationToken);

            var orderDtos = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                ServiceTitle = o.HomeServiceName?.Name,
                Price = o.BasePrice ?? 0,
                ImageUrl = o.HomeServiceName?.ImageUrl,
                Status = o.OrderStatus.ToString(),
                OrderDate = o.RequestDate
            }).ToList();

            return new OrderResultDto { Succeeded = true, Message = "لیست سفارش‌ها با موفقیت دریافت شد.", Orders = orderDtos };
        }
        public async Task<ResultDto> CompleteOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId, cancellationToken);
            if (order == null || order.OrderStatus != OrderStatus.WaitingForExpertArrival)
            {
                return new ResultDto { Succeeded = false, Message = "سفارش معتبر نیست یا در وضعیت درستی قرار ندارد." };
            }

            var Result = await _userService.GetCustomerByIdAsync(order.CustomerId, cancellationToken);
            var userCustomer = await _adminUserService.GetByIdAsync(Result.UserId, cancellationToken);
            if (userCustomer == null)
            {
                return new ResultDto { Succeeded = false, Message = "مشتری مرتبط با این سفارش یافت نشد." };
            }

            var proposals = await _viewOrderService.GetProposalsByOrderIdAsync(orderId, cancellationToken);
            var approvedProposal = proposals.FirstOrDefault(p => p.IsSelectedByCustomer);
            if (approvedProposal == null)
            {
                return new ResultDto { Succeeded = false, Message = "هیچ پیشنهادی تایید نشده است." };
            }

            var experts = await _adminUserAppService.GetExpertsListAsync(cancellationToken);
            var expert = experts.FirstOrDefault(e => e.Id == approvedProposal.ExpertId);
            if (expert == null)
            {
                return new ResultDto { Succeeded = false, Message = "اکسپرت مرتبط با پیشنهاد تایید شده یافت نشد." };
            }

            var expertUser = await _adminUserService.GetByIdAsync(expert.User.Id, cancellationToken);
            if (expertUser == null)
            {
                return new ResultDto { Succeeded = false, Message = "کاربر اکسپرت پیدا نشد." };
            }

            decimal transactionAmount = approvedProposal.ProposedPrice;
            decimal platformFee = transactionAmount * 0.05m;
            decimal customerAmount = transactionAmount + (transactionAmount * 0.025m);
            decimal expertAmount = transactionAmount - (transactionAmount * 0.025m);

            userCustomer.Balance -= customerAmount;
            if (userCustomer.Balance < 0)
            {
                return new ResultDto { Succeeded = false, Message = "موجودی کافی برای انجام تراکنش وجود ندارد." };
            }
            await _userService.UpdateUser(userCustomer, cancellationToken);

            expertUser.Balance += expertAmount;
            await _userService.UpdateUser(expertUser, cancellationToken);

            var adminUser = await _adminUserService.GetByIdAsync(1, cancellationToken);
            if (adminUser != null)
            {
                adminUser.Balance += platformFee;
                await _userService.UpdateUser(adminUser, cancellationToken);
            }

            order.OrderStatus = OrderStatus.Completed;
            order.PaymentStatus = PaymentStatus.Paid;
            await _orderService.UpdateOrderAsync(order, cancellationToken);

            return new ResultDto { Succeeded = true, Message = "سفارش با موفقیت تکمیل شد." };
        }

    }
}
