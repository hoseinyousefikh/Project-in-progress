using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace App.Domain.Core.Home.Contract.Repositories.Other
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetAllAsync(CancellationToken cancellationToken);
        Task<Comments> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(Comments comment, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Comments comment, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
