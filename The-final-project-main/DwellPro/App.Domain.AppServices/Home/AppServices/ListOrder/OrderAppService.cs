using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.ListOrder
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;

        public OrderAppService(IOrderService orderService)
        {
            _orderService = orderService;
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


    }
}
