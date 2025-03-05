using App.Domain.Core.Home.Contract.Repositories.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository.Users
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly AppDbContext _context;

        public ExpertRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Experts>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Experts
                .Include(e => e.User)
                .Include(e => e.ExpertProposals)
                .Include(e => e.ExpertHomeServices)
                .Include(e => e.Comments)
                .Include(e => e.Orders)
                .Include(e => e.HomeServices)
                .Where(e => !e.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Experts> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Experts
                .Include(e => e.User)
                .Include(e => e.ExpertProposals)
                .Include(e => e.ExpertHomeServices)
                .Include(e => e.Orders)
                .Include(e => e.HomeServices)
                .Where(e => e.UserId == id && !e.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (result != null)
            {
                return result;
            }

            throw new Exception("Expert not found");
        }
        public async Task<Experts> GetByEepertIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Experts
                .Include(e => e.User)
                .Include(e => e.ExpertProposals)
                .Include(e => e.ExpertHomeServices)
                .Include(e => e.Orders)
                .Include(e => e.HomeServices)
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (result != null)
            {
                return result;
            }

            throw new Exception("Expert not found");
        }
        public async Task<bool> AddAsync(Experts expert, CancellationToken cancellationToken)
        {
            await _context.Experts.AddAsync(expert, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Experts expert, CancellationToken cancellationToken)
        {
            _context.Experts.Update(expert);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var expert = await _context.Experts.FindAsync(id, cancellationToken);
            if (expert != null)
            {
                expert.IsDeleted = true;
                _context.Experts.Update(expert);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }

    }
}
