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
    public class ExpertHomeServiceRepository : IExpertHomeServiceRepository
    {
        private readonly AppDbContext _context;

        public ExpertHomeServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpertHomeService>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ExpertSkills
                .Include(ehs => ehs.Expert)
                .Include(ehs => ehs.HomeService)
                .Where(ehs => !ehs.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<ExpertHomeService> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.ExpertSkills
                .Include(ehs => ehs.Expert)
                .Include(ehs => ehs.HomeService)
                .Where(ehs => ehs.Id == id && !ehs.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("ExpertHomeService not found");
        }

        public async Task<bool> AddAsync(ExpertHomeService expertHomeService, CancellationToken cancellationToken)
        {
            await _context.ExpertSkills.AddAsync(expertHomeService, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(ExpertHomeService expertHomeService, CancellationToken cancellationToken)
        {
            _context.ExpertSkills.Update(expertHomeService);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var expertHomeService = await _context.ExpertSkills.FindAsync(id);
            if (expertHomeService != null)
            {
                expertHomeService.IsDeleted = true;
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result > 0;
            }
            return false;
        }
    }
}
