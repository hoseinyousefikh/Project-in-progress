using App.Domain.Core.Home.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.Categories
{
    public interface IHomeServiceRepository
    {
        Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken);
        Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(HomeService homeService, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
