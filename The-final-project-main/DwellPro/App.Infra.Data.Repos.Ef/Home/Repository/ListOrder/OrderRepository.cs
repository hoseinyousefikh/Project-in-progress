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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orders>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .ThenInclude(c => c.User)
                .Include(o => o.HomeServiceName)
                .Include(o => o.ExpertProposals)
                .ToListAsync(cancellationToken);
        }

        public async Task<Orders> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.HomeServiceName)
                .Include(o => o.ExpertProposals)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (result != null)
            {
                return result;
            }
            throw new Exception("is null");
        }

        public async Task<bool> AddAsync(Orders order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Orders order, CancellationToken cancellationToken)
        {
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.IsDeleted = true;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
    }
}
