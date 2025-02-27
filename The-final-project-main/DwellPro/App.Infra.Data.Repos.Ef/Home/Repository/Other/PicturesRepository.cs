using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Entities.Other;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace App.Infra.Data.Repos.Ef.Home.Repository.Other
{
    public class PicturesRepository : IPicturesRepository
    {
        private readonly AppDbContext _context;

        public PicturesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pictures>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Pictures
                .Include(p => p.Orders)
                .Where(p => !p.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Pictures> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Pictures
                .Include(p => p.Orders)
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);//*
            if (result != null)
            {
                return result;
            }
            throw new Exception("is null");
        }

        public async Task<bool> AddAsync(Pictures picture, CancellationToken cancellationToken)
        {
            await _context.Pictures.AddAsync(picture, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Pictures picture, CancellationToken cancellationToken)
        {
            _context.Pictures.Update(picture);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var picture = await _context.Pictures.FindAsync(id);
            if (picture != null)
            {
                picture.IsDeleted = true;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
    }
}
