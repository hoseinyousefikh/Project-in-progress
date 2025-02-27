using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Entities.Categories;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Microsoft.EntityFrameworkCore;
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

        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.SubCategories
                .Where(sc => !sc.IsDeleted)
                .Include(sc => sc.Category)
                .Include(sc => sc.HomeServices)
                .ToListAsync(cancellationToken);
        }

        public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.SubCategories
                .Include(sc => sc.Category)
                .Include(sc => sc.HomeServices)
                .FirstOrDefaultAsync(sc => sc.Id == id && !sc.IsDeleted, cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("SubCategory not found");
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
