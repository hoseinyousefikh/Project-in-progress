using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.Other;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository.Categories
{
    public class HomeServiceRepository : IHomeServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly string? _connectionString;


        public HomeServiceRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];

        }

        //public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    return await _context.HomeServices
        //                         .Include(h => h.SubCategory)
        //                         .ToListAsync(cancellationToken);
        //}

        //public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)
        //{
        //    var result = await _context.HomeServices
        //                                .Include(h => h.SubCategory)
        //                                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    throw new Exception("HomeService not found");
        //}


        public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var homeServices = await connection.QueryAsync<HomeService>(
                HomeServiceQueries.GetAllHomeServices
            );

            var homeServiceList = homeServices.ToList();

            foreach (var homeService in homeServiceList)
            {
                var subCategory = await connection.QueryFirstOrDefaultAsync<SubCategory>(
                    HomeServiceQueries.GetSubCategoryForHomeService,
                    new { SubCategoryId = homeService.SubCategoryId }
                );
                homeService.SubCategory = subCategory;

                var expertHomeServices = await connection.QueryAsync<ExpertHomeService>(
                    HomeServiceQueries.GetExpertHomeServicesForHomeService,
                    new { HomeServiceId = homeService.Id }
                );
                homeService.ExpertHomeServices = expertHomeServices.ToList();
            }

            return homeServiceList;
        }

        public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var homeService = await connection.QueryFirstOrDefaultAsync<HomeService>(
                HomeServiceQueries.GetHomeServiceById,
                new { Id = id }
            );

            if (homeService == null)
            {
                throw new Exception("HomeService not found");
            }

            var subCategory = await connection.QueryFirstOrDefaultAsync<SubCategory>(
                HomeServiceQueries.GetSubCategoryForHomeService,
                new { SubCategoryId = homeService.SubCategoryId }
            );
            homeService.SubCategory = subCategory;

            var expertHomeServices = await connection.QueryAsync<ExpertHomeService>(
                HomeServiceQueries.GetExpertHomeServicesForHomeService,
                new { HomeServiceId = id }
            );
            homeService.ExpertHomeServices = expertHomeServices.ToList();

            return homeService;
        }
    

        public async Task<bool> AddAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            await _context.HomeServices.AddAsync(homeService, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            _context.HomeServices.Update(homeService);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var homeService = await _context.HomeServices.FindAsync(new object[] { id }, cancellationToken);
            if (homeService != null)
            {
                homeService.IsDeleted = true;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
    }
}
