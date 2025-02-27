using App.Domain.Core.Home.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.Users
{
    public interface IExpertRepository
    {
        Task<Experts> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Experts>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> AddAsync(Experts expert, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Experts expert, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
