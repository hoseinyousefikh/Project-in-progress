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
    public class HomeServiceRepository : IHomeServiceRepository
    {
        private readonly AppDbContext _context;

        public HomeServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.HomeServices
                                 .Include(h => h.SubCategory)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.HomeServices
                                        .Include(h => h.SubCategory)
                                        .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("HomeService not found");
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
