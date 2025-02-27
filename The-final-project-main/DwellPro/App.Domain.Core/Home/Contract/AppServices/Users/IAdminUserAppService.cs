using App.Domain.Core.Home.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.Users
{
    public interface IAdminUserAppService
    {
        Task<IdentityResult> LoginAdminAsync(string username, string password, bool rememberMe);
        Task<IdentityResult> UpdateAdminAsync(int userId, string firstName, string lastName, int? cityId, string? profilePicture, string? description, string? address, string? shebaNumber, string? cardNumber, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAdminPasswordAsync(int userId, string currentPassword, string newPassword, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAdminEmailAsync(int userId, string newEmail, CancellationToken cancellationToken);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetByIdAsync(int userId, CancellationToken cancellationToken);
        Task SignOutAdminAsync();
        Task<List<Experts>> GetExpertsListAsync(CancellationToken cancellationToken);
        Task<List<Customers>> GetCustomersListAsync(CancellationToken cancellationToken);
        Task<string> EditProfileAndUploadImage(int userId, IFormFile profilePicture, string existingImageUrl, CancellationToken cancellationToken);
    }
}
