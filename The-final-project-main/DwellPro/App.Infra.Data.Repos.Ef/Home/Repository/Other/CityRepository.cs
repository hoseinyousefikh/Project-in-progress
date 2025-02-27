using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Entities.Other;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace App.Infra.Data.Repos.Ef.Home.Repository.Other
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Cities
                .Include(c => c.Users)
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<City> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Cities
                .Include(c => c.Users)
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("City not found");
        }

        public async Task<bool> AddAsync(City city, CancellationToken cancellationToken)
        {
            await _context.Cities.AddAsync(city, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(City city, CancellationToken cancellationToken)
        {
            _context.Cities.Update(city);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                city.IsDeleted = true;
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result > 0;
            }
            return false;
        }
    }
}
