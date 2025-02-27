using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.Categories
{
    public class HomeServiceService : IHomeServiceService
    {
        private readonly IHomeServiceRepository _homeServiceRepository;

        public HomeServiceService(IHomeServiceRepository homeServiceRepository)
        {
            _homeServiceRepository = homeServiceRepository;
        }

        public async Task<bool> AddHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            if (homeService == null)
                throw new ArgumentNullException(nameof(homeService));

            return await _homeServiceRepository.AddAsync(homeService, cancellationToken);
        }

        public async Task<List<HomeService>> GetAllHomeServicesAsync(CancellationToken cancellationToken)
        {
            var homeServices = await _homeServiceRepository.GetAllAsync(cancellationToken);

            return homeServices.Where(service => !service.IsDeleted).ToList();
        }


        public async Task<HomeService> GetHomeServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _homeServiceRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            if (homeService == null)
                throw new ArgumentNullException(nameof(homeService));

            return await _homeServiceRepository.UpdateAsync(homeService, cancellationToken);
        }

        public async Task<bool> DeleteHomeServiceAsync(int id, CancellationToken cancellationToken)
        {
            return await _homeServiceRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
