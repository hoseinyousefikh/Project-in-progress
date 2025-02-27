using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace App.Domain.Core.Home.Contract.Repositories.Other
{
    public interface IPicturesRepository
    {
        Task<List<Pictures>> GetAllAsync(CancellationToken cancellationToken);
        Task<Pictures> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(Pictures picture, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Pictures picture, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
