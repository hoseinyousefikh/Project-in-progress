using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.ListOrder
{
    public interface IOrderAppService
    {
        Task<bool> AddOrderAsync(Orders order, CancellationToken cancellationToken);
        Task<List<Orders>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<Orders> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateOrderAsync(Orders order, CancellationToken cancellationToken);
        Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken);
        Task<List<Orders>> GetOrdersByStatusAndCustomerIdAsync(OrderStatus status, int customerId, CancellationToken cancellationToken);
        Task<CreateOrderDto> GetCreateOrderDataAsync(int homeServiceId, CancellationToken cancellationToken);
        Task<ResultDto> CreateOrderAsync(CreateOrderDto orderDto, ClaimsPrincipal user, CancellationToken cancellationToken);
        Task<List<Orders>> GetOrdersForUserAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
        Task<ProposalDto> GetProposalsForOrderAsync(int orderId, CancellationToken cancellationToken);
        Task<OrderResultDto> GetAllOrderByStatusAsync(int userId, CancellationToken cancellationToken);
        Task<ResultDto> CompleteOrderAsync(int orderId, CancellationToken cancellationToken);
    }
}
