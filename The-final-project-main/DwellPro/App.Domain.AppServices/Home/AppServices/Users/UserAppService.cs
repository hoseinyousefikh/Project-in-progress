using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AdminUserAppService> _logger;

        public UserAppService(IUserService userService, SignInManager<User> signInManager, ILogger<AdminUserAppService> logger)
        {
            _userService = userService;
            _logger = logger;
            _signInManager = signInManager;
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

        public async Task<IdentityResult> UpdatePasswordAsync(int userId, string currentPassword, string newPassword, string confirmPassword, CancellationToken cancellationToken)
        {
            return await _userService.UpdatePasswordAsync(userId, currentPassword, newPassword, confirmPassword, cancellationToken);
        }


        public Task<IdentityResult> UpdateEmailAsync(int userId, string newEmail, CancellationToken cancellationToken)
        {
            return _userService.UpdateEmailAsync(userId, newEmail, cancellationToken);
        }

        public Task<IdentityResult> DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            return _userService.DeleteUserAsync(userId, cancellationToken);
        }
        public async Task SignOutCustomerAsync()
        {
            try
            {
                _logger.LogInformation("Customer user is signing out.");

                await _signInManager.SignOutAsync();

                _logger.LogInformation("Customer user signed out successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while signing out the customer user.");
                throw;
            }
        }
        public async Task<IdentityResult> UpdateUserAsync(
                                        int userId,
                                        string firstName,
                                        string lastName,
                                        int? cityId,
                                        string? profilePicture,
                                        string? description,
                                        string? address,
                                        string? shebaNumber,
                                        string? cardNumber,
                                        CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserAsync(userId, firstName, lastName, cityId, profilePicture, description, address, shebaNumber, cardNumber, cancellationToken);
        }

        public async Task<Experts> GetExpertByIdAsync(int id, CancellationToken cancellationToken)
        {
            var expert = await _userService.GetExpertByIdAsync(id, cancellationToken);
            if (expert != null && !expert.IsDeleted && expert.RoleStatus == UserStatus.Active)
            {
                return expert;
            }
            throw new Exception("Expert not found, not approved, deleted, or inactive");
        }
    }
}
