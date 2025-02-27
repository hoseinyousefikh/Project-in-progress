using App.Domain.Core.Home.Contract.Repositories.ListOrder;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository.ListOrder
{
    public class ExpertProposalRepository : IExpertProposalRepository
    {
        private readonly AppDbContext _context;

        public ExpertProposalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpertProposal>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ExpertProposals
                .Include(ep => ep.Order)
                .Include(ep => ep.Expert)
                .ThenInclude(c => c.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<ExpertProposal> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.ExpertProposals
                .Include(ep => ep.Order)
                .Include(ep => ep.Expert)
                .FirstOrDefaultAsync(ep => ep.Id == id, cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("is null");
        }

        public async Task<bool> AddAsync(ExpertProposal expertProposal, CancellationToken cancellationToken)
        {
            await _context.ExpertProposals.AddAsync(expertProposal, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(ExpertProposal expertProposal, CancellationToken cancellationToken)
        {
            _context.ExpertProposals.Update(expertProposal);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var expertProposal = await _context.ExpertProposals.FindAsync(id);
            if (expertProposal != null)
            {
                expertProposal.IsDeleted = true;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
    }
}
