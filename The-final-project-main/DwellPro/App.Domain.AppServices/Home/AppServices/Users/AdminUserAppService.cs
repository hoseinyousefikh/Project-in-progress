using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.Users
{
    public class AdminUserAppService : IAdminUserAppService
    {
        private readonly IAdminUserService _adminUserService;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AdminUserAppService> _logger;
        public AdminUserAppService(IAdminUserService adminUserService, SignInManager<User> signInManager, ILogger<AdminUserAppService> logger)
        {
            _adminUserService = adminUserService;
            _signInManager = signInManager;
            _logger = logger;
        }

        public Task<IdentityResult> LoginAdminAsync(string username, string password, bool rememberMe)
        {
            return _adminUserService.LoginAdminAsync(username, password, rememberMe);
        }

        public Task<IdentityResult> UpdateAdminAsync(int userId, string firstName, string lastName, int? cityId, string? profilePicture, string? description, string? address, string? shebaNumber, string? cardNumber, CancellationToken cancellationToken)
        {
            return _adminUserService.UpdateAdminAsync(userId, firstName, lastName, cityId, profilePicture, description, address, shebaNumber, cardNumber, cancellationToken);
        }

        public Task<IdentityResult> UpdateAdminPasswordAsync(int userId, string currentPassword, string newPassword, CancellationToken cancellationToken)
        {
            return _adminUserService.UpdateAdminPasswordAsync(userId, currentPassword, newPassword, cancellationToken);
        }

        public Task<IdentityResult> UpdateAdminEmailAsync(int userId, string newEmail, CancellationToken cancellationToken)
        {
            return _adminUserService.UpdateAdminEmailAsync(userId, newEmail, cancellationToken);
        }

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _adminUserService.GetAllAsync(cancellationToken);
        }

        public Task<User> GetByIdAsync(int userId, CancellationToken cancellationToken)
        {
            return _adminUserService.GetByIdAsync(userId, cancellationToken);
        }
        public async Task SignOutAdminAsync()
        {
            try
            {
                _logger.LogInformation("Admin user is signing out.");

                await _signInManager.SignOutAsync();

                _logger.LogInformation("Admin user signed out successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while signing out the admin user.");
                throw;
            }
        }
        public async Task<List<Experts>> GetExpertsListAsync(CancellationToken cancellationToken)
        {
            var users = await _adminUserService.GetAllAsync(cancellationToken);

            var experts = users
                .Where(user => user.RoleType == RoleEnum.Expert)
                .Select(user => new Experts
                {
                    Id = user.ExpertDetails?.Id ?? 0,
                    User = user,
                    Rating = user.ExpertDetails?.Rating ?? 0,
                    Biography = user.ExpertDetails?.Biography,
                    RoleStatus = user.ExpertDetails?.RoleStatus ?? UserStatus.inActive
                })
                .ToList();

            return experts;
        }
        public async Task<List<Customers>> GetCustomersListAsync(CancellationToken cancellationToken)
        {
            var users = await _adminUserService.GetAllAsync(cancellationToken);
                  var customers = users
                            .Where(user => user.RoleType == RoleEnum.Customer)
                            .Select(user => new Customers
                            {
                                Id = user.CustomerDetails?.Id ?? 0,
                                User = user,
                                RoleStatus = user.CustomerDetails?.RoleStatus ?? UserStatus.inActive,
                                IsDeleted = user.CustomerDetails?.IsDeleted ?? false
                            })
                            .ToList();
            return customers;
        }

        public async Task<string> EditProfileAndUploadImage(int userId, IFormFile profilePicture, string existingImageUrl, CancellationToken cancellationToken)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                return existingImageUrl;
            }

            string profileImagePath = existingImageUrl;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(fileStream, cancellationToken);
            }

            if (!string.IsNullOrEmpty(existingImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            profileImagePath = "/uploads/" + fileName;

            return profileImagePath;
        }

    }
}
