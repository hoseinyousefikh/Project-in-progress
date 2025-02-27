using App.Domain.Core.Home.Entities.ListOrder;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.ListOrder
{
    public interface IOrderRepository
    {
        Task<List<Orders>> GetAllAsync(CancellationToken cancellationToken);
        Task<Orders> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(Orders order, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Orders order, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
