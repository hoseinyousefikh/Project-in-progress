using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.Other
{
    public class CityAppService : ICityAppService
    {
        private readonly ICityService _cityService;

        public CityAppService(ICityService cityService)
        {
            _cityService = cityService;
        }

        public Task<bool> AddCityAsync(City city, CancellationToken cancellationToken)
        {
            return _cityService.AddCityAsync(city, cancellationToken);
        }

        public Task<List<City>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            return _cityService.GetAllCitiesAsync(cancellationToken);
        }

        public Task<City> GetCityByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _cityService.GetCityByIdAsync(id, cancellationToken);
        }

        public Task<bool> UpdateCityAsync(City city, CancellationToken cancellationToken)
        {
            return _cityService.UpdateCityAsync(city, cancellationToken);
        }

        public Task<bool> DeleteCityAsync(int id, CancellationToken cancellationToken)
        {
            return _cityService.DeleteCityAsync(id, cancellationToken);
        }
    }
}
