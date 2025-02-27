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
    public class CustomersRepository : ICustomersRepository
    {
        private readonly AppDbContext _context;

        public CustomersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customers>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Orders)
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Customers> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Orders)
                .Where(c => c.UserId == id && !c.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (result != null)
            {
                return result;
            }

            throw new Exception("Customer not found.");
        }

        public async Task<bool> AddAsync(Customers customer, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(customer, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Customers customer, CancellationToken cancellationToken)
        {
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(id, cancellationToken);
            if (customer != null)
            {
                customer.IsDeleted = true;
                _context.Customers.Update(customer);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }

    }
}
