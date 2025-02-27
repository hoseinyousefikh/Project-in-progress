using App.Domain.Core.Home.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<IdentityResult> AddAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
