using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Entities.Categories;
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

namespace App.Infra.Data.Repos.Ef.Home.Repository.Categories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly string? _connectionString;


        public SubCategoryRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];

        }

        //public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    return await _context.SubCategories
        //        .Where(sc => !sc.IsDeleted)
        //        .Include(sc => sc.Category)
        //        .Include(sc => sc.HomeServices)
        //        .ToListAsync(cancellationToken);
        //}

        //public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)
        //{
        //    var result = await _context.SubCategories
        //        .Include(sc => sc.Category)
        //        .Include(sc => sc.HomeServices)
        //        .FirstOrDefaultAsync(sc => sc.Id == id && !sc.IsDeleted, cancellationToken);
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    throw new Exception("SubCategory not found");
        //}
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var subCategories = await connection.QueryAsync<SubCategory>(
                SubCategoryQueries.GetAllSubCategories
            );

            var subCategoryList = subCategories.ToList();

            foreach (var subCategory in subCategoryList)
            {
                var category = await connection.QueryFirstOrDefaultAsync<Category>(
                    SubCategoryQueries.GetCategoryForSubCategory,
                    new { CategoryId = subCategory.CategoryId }
                );
                subCategory.Category = category;

                var homeServices = await connection.QueryAsync<HomeService>(
                    SubCategoryQueries.GetHomeServicesForSubCategory,
                    new { SubCategoryId = subCategory.Id }
                );
                subCategory.HomeServices = homeServices.ToList();
            }

            return subCategoryList;
        }

        public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var subCategory = await connection.QueryFirstOrDefaultAsync<SubCategory>(
                SubCategoryQueries.GetSubCategoryById,
                new { Id = id }
            );

            if (subCategory == null)
            {
                throw new Exception("SubCategory not found");
            }

            var category = await connection.QueryFirstOrDefaultAsync<Category>(
                SubCategoryQueries.GetCategoryForSubCategory,
                new { CategoryId = subCategory.CategoryId }
            );
            subCategory.Category = category;

            var homeServices = await connection.QueryAsync<HomeService>(
                SubCategoryQueries.GetHomeServicesForSubCategory,
                new { SubCategoryId = id }
            );
            subCategory.HomeServices = homeServices.ToList();

            return subCategory;
        }

        public async Task<bool> AddAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            await _context.SubCategories.AddAsync(subCategory, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            _context.SubCategories.Update(subCategory);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var subCategory = await _context.SubCategories.FindAsync(new object[] { id }, cancellationToken);
            if (subCategory != null)
            {
                subCategory.IsDeleted = true;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
    }
}
