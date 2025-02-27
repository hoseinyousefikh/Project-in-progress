using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public Task<IdentityResult> RegisterAsync(
            string firstName, string lastName, string email, string password,
            string confirmPassword, int cityId, RoleEnum roleType, CancellationToken cancellationToken)
        {
            return _userService.RegisterAsync(firstName, lastName, email, password, confirmPassword, cityId, roleType, cancellationToken);
        }

        public Task<IdentityResult> Login(string username, string password, bool rememberMe)
        {
            return _userService.Login(username, password, rememberMe);
        }

        public Task<IdentityResult> UpdateUserAsync(
            int userId, string firstName, string lastName, int? cityId, string? profilePicture,
            string? description, string? address, string? shebaNumber, string? cardNumber,
            UserStatus roleStatus, CancellationToken cancellationToken)
        {
            return _userService.UpdateUserAsync(userId, firstName, lastName, cityId, profilePicture, description, address, shebaNumber, cardNumber, roleStatus, cancellationToken);
        }

        public Task<IdentityResult> UpdatePasswordAsync(int userId, string newPassword, string confirmPassword, CancellationToken cancellationToken)
        {
            return _userService.UpdatePasswordAsync(userId, newPassword, confirmPassword, cancellationToken);
        }

        public Task<IdentityResult> UpdateEmailAsync(int userId, string newEmail, CancellationToken cancellationToken)
        {
            return _userService.UpdateEmailAsync(userId, newEmail, cancellationToken);
        }

        public Task<IdentityResult> DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            return _userService.DeleteUserAsync(userId, cancellationToken);
        }
    }
}
