using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.Users
{
    public interface IUserAppService
    {
        Task<IdentityResult> RegisterAsync(
            string firstName, string lastName, string email, string password,
            string confirmPassword, int cityId, RoleEnum roleType, CancellationToken cancellationToken);

        Task<IdentityResult> Login(string username, string password, bool rememberMe);

        Task<IdentityResult> UpdateUserAsync(
            int userId, string firstName, string lastName, int? cityId, string? profilePicture,
            string? description, string? address, string? shebaNumber, string? cardNumber,
            UserStatus roleStatus, CancellationToken cancellationToken);

        Task<IdentityResult> UpdatePasswordAsync(int userId, string currentPassword, string newPassword, string confirmPassword, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateEmailAsync(int userId, string newEmail, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteUserAsync(int userId, CancellationToken cancellationToken);
        Task SignOutCustomerAsync();
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
        Task<ResultDto> EditUserAsync(int userId, EditUserDto model, IFormFile profilePicture, CancellationToken cancellationToken);
        Task<UserDetailsDto> GetUserDetailsAsync(int userId, CancellationToken cancellationToken);
        Task<EditUserDto> GetEditUserDataAsync(int userId, CancellationToken cancellationToken);
    }
}
