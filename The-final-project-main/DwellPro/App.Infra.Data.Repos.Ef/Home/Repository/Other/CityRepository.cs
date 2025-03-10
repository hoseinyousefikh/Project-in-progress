using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository.Other
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        private readonly string? _connectionString;

        public CityRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];

        }

        //public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    return await _context.Cities
        //        .Include(c => c.Users)
        //        .Where(c => !c.IsDeleted)
        //        .ToListAsync(cancellationToken);
        //}

        //public async Task<City> GetByIdAsync(int id, CancellationToken cancellationToken)
        //{
        //    var result = await _context.Cities
        //        .Include(c => c.Users)
        //        .Where(c => c.Id == id && !c.IsDeleted)
        //        .FirstOrDefaultAsync(cancellationToken);
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    throw new Exception("City not found");
        //}

        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var cities = await connection.QueryAsync<City>(CityQueries.GetAllCities, cancellationToken);

            foreach (var city in cities)
            {
                var users = await connection.QueryAsync<User>(CityQueries.GetUsersForCity, new { CityId = city.Id });
                city.Users = users.ToList();
            }

            return cities.ToList();
        }


        public async Task<City> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var city = await connection.QueryFirstOrDefaultAsync<City>(
                CityQueries.GetCityById,
                new { Id = id }
            );
            if (city == null)
            {
                throw new Exception("City not found");
            }
            var users = await connection.QueryAsync<User>(
                CityQueries.GetUsersForCityId,
                new { CityId = id }
            );

            city.Users = users.ToList();

            return city;
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
