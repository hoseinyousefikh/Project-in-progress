using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Entities.Categories;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                                 .Include(c => c.SubCategories)
                                 .Where(c => !c.IsDeleted)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Categories
                                        .Include(c => c.SubCategories)
                                        .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("Category not found");
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
