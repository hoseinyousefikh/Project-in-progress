using App.Domain.Core.Home.Contract.AppServices.ListOrder.Order;
using App.Domain.Core.Home.Contract.Services.ListOrder.Order;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.ListOrder.Order
{
    public class OrderManagementAppService : IOrderManagementAppService
    {
        private readonly IOrderManagementService _orderManagementService;

        public OrderManagementAppService(IOrderManagementService orderManagementService)
        {
            _orderManagementService = orderManagementService;
        }

        public Task<List<Orders>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return _orderManagementService.GetAllOrdersAsync(cancellationToken);
        }

        public Task<Orders> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return _orderManagementService.GetOrderByIdAsync(orderId, cancellationToken);
        }

        public Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, CancellationToken cancellationToken)
        {
            return _orderManagementService.UpdateOrderStatusAsync(orderId, newStatus, cancellationToken);
        }

        public Task UpdatePaymentStatusAsync(int orderId, PaymentStatus newPaymentStatus, CancellationToken cancellationToken)
        {
            return _orderManagementService.UpdatePaymentStatusAsync(orderId, newPaymentStatus, cancellationToken);
        }
    }
}
