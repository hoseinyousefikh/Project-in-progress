using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.ListOrder.Order
{
    public interface IOrderManagementAppService
    {
        Task<List<Orders>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<Orders> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, CancellationToken cancellationToken);
        Task UpdatePaymentStatusAsync(int orderId, PaymentStatus newPaymentStatus, CancellationToken cancellationToken);
    }
}
