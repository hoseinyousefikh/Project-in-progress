using App.Domain.Core.Home.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.Categories
{
    public interface IHomeServiceService
    {
        Task<bool> AddHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken);
        Task<List<HomeService>> GetAllHomeServicesAsync(CancellationToken cancellationToken);
        Task<HomeService> GetHomeServiceByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken);
        Task<bool> DeleteHomeServiceAsync(int id, CancellationToken cancellationToken);
    }
}
