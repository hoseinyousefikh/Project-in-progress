using App.Domain.Core.Home.Contract.Repositories.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Entities.ListOrder;

namespace App.Domain.Services.Home.Services.ListOrder
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> AddOrderAsync(Orders order, CancellationToken cancellationToken)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            return await _orderRepository.AddAsync(order, cancellationToken);
        }

        public async Task<List<Orders>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Orders> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateOrderAsync(Orders order, CancellationToken cancellationToken)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            return await _orderRepository.UpdateAsync(order, cancellationToken);
        }

        public async Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
