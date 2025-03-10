using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Entities.Categories;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly string? _connectionString;

        public CategoryRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];

        }

        //public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    return await _context.Categories
        //                         .Include(c => c.SubCategories)
        //                         .Where(c => !c.IsDeleted)
        //                         .ToListAsync(cancellationToken);
        //}

        //public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
        //{
        //    var result = await _context.Categories
        //                                .Include(c => c.SubCategories)
        //                                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, cancellationToken);
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    throw new Exception("Category not found");
        //}

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var categories = await connection.QueryAsync<Category>(
                CategoryQueries.GetAllCategories
            );

            var categoryList = categories.ToList();

            foreach (var category in categoryList)
            {
                var subcategories = await connection.QueryAsync<SubCategory>(
                    CategoryQueries.GetSubCategoriesForCategory,
                    new { CategoryId = category.Id }
                );

                category.SubCategories = subcategories.ToList();
            }

            return categoryList;
        }
        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var category = await connection.QueryFirstOrDefaultAsync<Category>(
                CategoryQueries.GetCategoryById,
                new { Id = id }
            );

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var subcategories = await connection.QueryAsync<SubCategory>(
                CategoryQueries.GetSubCategoriesForCategoryId,
                new { CategoryId = id }
            );

            category.SubCategories = subcategories.ToList();

            return category;
        }


        public async Task<bool> AddAsync(Category category, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
            if (category != null)
            {
                category.IsDeleted = true;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
    }
}
