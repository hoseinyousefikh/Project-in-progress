using App.Domain.Core.Home.Contract.Repositories.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder.Order;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.ListOrder.Order
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManagementService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Orders>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);
            return orders.Where(o => !o.IsDeleted).ToList();
        }

        public async Task<Orders> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Order not found");
            if (order.IsDeleted)
                throw new Exception("This order has been deleted.");

            return order;
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Order not found");
            if (order.IsDeleted)
                throw new InvalidOperationException("This order has been deleted and its status cannot be changed.");
            if (!order.IsApproved)
                throw new InvalidOperationException("This order is not approved yet and its status cannot be changed.");

          

            order.OrderStatus = newStatus;
            await _orderRepository.UpdateAsync(order, cancellationToken);
        }

        public async Task UpdatePaymentStatusAsync(int orderId, PaymentStatus newPaymentStatus, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Order not found");
            if (order.IsDeleted)
                throw new InvalidOperationException("This order has been deleted and its payment status cannot be changed.");

            order.PaymentStatus = newPaymentStatus;
            await _orderRepository.UpdateAsync(order, cancellationToken);
        }

        //private bool IsValidStatusChange(OrderStatus currentStatus, OrderStatus newStatus, PaymentStatus paymentStatus)
        //{
        //    if (newStatus == OrderStatus.Completed && paymentStatus != PaymentStatus.Paid)
        //    {
        //        return false;
        //    }

        //    var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
        //    {
        //        { OrderStatus.WaitingForExpertProposal, new List<OrderStatus> { OrderStatus.WaitingForExpertSelection } },
        //        { OrderStatus.WaitingForExpertSelection, new List<OrderStatus> { OrderStatus.WaitingForExpertArrival } },
        //        { OrderStatus.WaitingForExpertArrival, new List<OrderStatus> { OrderStatus.Completed } }
        //    };

        //    return validTransitions.ContainsKey(currentStatus) && validTransitions[currentStatus].Contains(newStatus);
        //}
    }
}
