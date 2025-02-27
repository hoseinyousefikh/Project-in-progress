using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace App.Domain.Core.Home.Contract.Repositories.Other
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
        Task<City> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(City city, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(City city, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
