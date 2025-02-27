using App.Domain.Core.Home.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.Users
{
    public interface ICustomersRepository
    {
        Task<List<Customers>> GetAllAsync(CancellationToken cancellationToken);
        Task<Customers> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(Customers customer, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Customers customer, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
