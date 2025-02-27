using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.Other
{
    public interface ICityService
    {
        Task<bool> AddCityAsync(City city, CancellationToken cancellationToken);
        Task<List<City>> GetAllCitiesAsync(CancellationToken cancellationToken);
        Task<City> GetCityByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateCityAsync(City city, CancellationToken cancellationToken);
        Task<bool> DeleteCityAsync(int id, CancellationToken cancellationToken);
    }
}
