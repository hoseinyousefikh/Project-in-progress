using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace App.Domain.Core.Home.Contract.Repositories.Other
{
    public interface IExpertHomeServiceRepository
    {
        Task<List<ExpertHomeService>> GetAllAsync(CancellationToken cancellationToken);
        Task<ExpertHomeService> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(ExpertHomeService expertHomeService, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(ExpertHomeService expertHomeService, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
