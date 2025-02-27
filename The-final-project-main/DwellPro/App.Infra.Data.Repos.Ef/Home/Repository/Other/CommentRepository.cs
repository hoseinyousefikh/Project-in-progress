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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comments>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Comments
                .Include(c => c.Customers)
                .Include(c => c.Experts) 
                .ThenInclude(e => e.User)
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Comments> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Comments
                .Include(c => c.Customers)
                .Include(c => c.Experts)
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("Comment not found");
        }

        public async Task<bool> AddAsync(Comments comment, CancellationToken cancellationToken)
        {
            await _context.Comments.AddAsync(comment, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Comments comment, CancellationToken cancellationToken)
        {
            _context.Comments.Update(comment);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                comment.IsDeleted = true;
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result > 0;
            }
            return false;
        }
    }
}
