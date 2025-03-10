using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;

namespace App.Domain.Services.Home.Services.Other
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<bool> AddCityAsync(City city, CancellationToken cancellationToken)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));

            return await _cityRepository.AddAsync(city, cancellationToken);
        }

        public async Task<List<City>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
                return await _cityRepository.GetAllAsync(cancellationToken);
        }

        public async Task<City> GetCityByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _cityRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateCityAsync(City city, CancellationToken cancellationToken)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));

            return await _cityRepository.UpdateAsync(city, cancellationToken);
        }

        public async Task<bool> DeleteCityAsync(int id, CancellationToken cancellationToken)
        {
            return await _cityRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
