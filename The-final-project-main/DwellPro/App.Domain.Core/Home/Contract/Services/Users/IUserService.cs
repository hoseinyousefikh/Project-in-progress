using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
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
        Task<IdentityResult> UpdatePasswordAsync(int userId, string currentPassword, string newPassword, string confirmPassword, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateEmailAsync(int userId, string newEmail, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteUserAsync(int userId, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateUserAsync(
                                        int userId,
                                        string firstName,
                                        string lastName,
                                        int? cityId,
                                        string? profilePicture,
                                        string? description,
                                        string? address,
                                        string? shebaNumber,
                                        string? cardNumber,
                                        CancellationToken cancellationToken);
        Task<Experts> GetExpertByIdAsync(int id, CancellationToken cancellationToken);
        Task<Customers> GetCustomerByIdAsync(int id, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateUser(User user, CancellationToken cancellationToken);
        Task<bool> UpdateExpertAsync(Experts expert, CancellationToken cancellationToken);
        Task<List<ExpertHomeService>> GetByExpertIdAsync(int expertId, CancellationToken cancellationToken);
    }
}
