using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
