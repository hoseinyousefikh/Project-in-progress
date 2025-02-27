using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.Users
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(string firstName, string lastName, string email, string password, string confirmPassword, int cityId, RoleEnum roleType, CancellationToken cancellationToken);
        Task<IdentityResult> Login(string username, string password, bool rememberMe);
        Task<IdentityResult> UpdateUserAsync(int userId, string firstName, string lastName, int? cityId, string? profilePicture, string? description, string? address, string? shebaNumber, string? cardNumber, UserStatus roleStatus, CancellationToken cancellationToken);
        Task<IdentityResult> UpdatePasswordAsync(int userId, string newPassword, string confirmPassword, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateEmailAsync(int userId, string newEmail, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteUserAsync(int userId, CancellationToken cancellationToken);
    }
}
