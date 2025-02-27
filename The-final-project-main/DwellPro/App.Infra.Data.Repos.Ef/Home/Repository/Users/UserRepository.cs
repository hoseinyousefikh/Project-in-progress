using App.Domain.Core.Home.Contract.Repositories.Users;
using App.Domain.Core.Home.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userManager.Users
                .Include(u => u.City)
                .Include(u => u.ExpertDetails)
                .Include(u => u.CustomerDetails)
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.City)
                .Include(u => u.ExpertDetails)
                .Include(u => u.CustomerDetails)
                .Where(u => u.Id == id && !u.IsDeleted)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return user;
            }
            throw new Exception("User not found");
        }

        public async Task<IdentityResult> AddAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.IsDeleted = true;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
