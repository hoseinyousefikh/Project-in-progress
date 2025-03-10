using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.Other;
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
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AdminUserAppService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IAdminUserAppService _adminUserAppService;
        private readonly ICityService _cityService;
        private readonly IExpertProposalService _expertProposalService;
        public UserAppService(IUserService userService, SignInManager<User> signInManager, ILogger<AdminUserAppService> logger, UserManager<User> userManager , IAdminUserAppService adminUserAppService , ICityService cityService, IExpertProposalService expertProposalService)
        {
            _userService = userService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _adminUserAppService = adminUserAppService;
            _cityService = cityService;
            _expertProposalService = expertProposalService;
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

        public Task<IdentityResult> UpdateUser(User user, CancellationToken cancellationToken)
        {
            return _userService.UpdateUser(user,cancellationToken);
        }

        public Task<Customers> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _userService.GetCustomerByIdAsync(id, cancellationToken);
        }

        public Task<bool> UpdateExpertAsync(Experts expert, CancellationToken cancellationToken)
        {
            return _userService.UpdateExpertAsync(expert ,cancellationToken);
        }

        public async Task<ResultDto> EditUserAsync(int userId, EditUserDto model, IFormFile profilePicture, CancellationToken cancellationToken)
        {
            var result = new ResultDto();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                result.Succeeded = false;
                result.Message = "کاربر پیدا نشد.";
                return result;
            }

            UserStatus currentStatus = model.RoleStatus;
            if (currentStatus == null)
            {
                currentStatus = user.RoleType == RoleEnum.Customer && user.CustomerDetails != null
                    ? user.CustomerDetails.RoleStatus
                    : user.RoleType == RoleEnum.Expert && user.ExpertDetails != null
                        ? user.ExpertDetails.RoleStatus
                        : throw new Exception("نقش کاربر نامعتبر است.");
            }

            model.ProfilePicture = await _adminUserAppService.EditProfileAndUploadImage(
                userId,
                profilePicture,
                user.ProfilePicture,
                cancellationToken
            );

            var updateResult = await _userService.UpdateUserAsync(
                userId,
                model.FirstName,
                model.LastName,
                model.CityId,
                model.ProfilePicture,
                model.Description,
                model.Address,
                model.ShebaNumber,
                model.CardNumber,
                cancellationToken
            );

            result.Succeeded = updateResult.Succeeded;
            result.Message = updateResult.Succeeded ? "ویرایش با موفقیت انجام شد." : "خطا در به‌روزرسانی اطلاعات کاربر.";

            return result;
        }
        public async Task<UserDetailsDto> GetUserDetailsAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _adminUserAppService.GetByIdAsync(userId, cancellationToken);

            if (user == null)
            {
                return null;
            }

            if (user.CityId.HasValue)
            {
                user.City = await _cityService.GetCityByIdAsync(user.CityId.Value, cancellationToken);
            }

            return new UserDetailsDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityName = user.City?.Name ?? "نامشخص",
                ProfilePicture = user.ProfilePicture,
                Description = user.Description,
                Address = user.Address,
                ShebaNumber = user.ShebaNumber,
                CardNumber = user.CardNumber,
                Balance = user.Balance,
                RoleStatus = user.RoleType == RoleEnum.Customer
                    ? (user.CustomerDetails?.RoleStatus ?? UserStatus.inActive)
                    : (user.ExpertDetails?.RoleStatus ?? UserStatus.inActive)
            };
        }


        public async Task<ExpertDetailsDto> GetExpertDetailsAsync(int expertId, CancellationToken cancellationToken)
        {
            var expert = await _adminUserAppService.GetByIdAsync(expertId, cancellationToken);

            if (expert == null)
            {
                return null;
            }

            var user = await _adminUserAppService.GetByIdAsync(expert.Id, cancellationToken);

            if (user == null)
            {
                return null;
            }

            if (user.CityId.HasValue)
            {
                user.City = await _cityService.GetCityByIdAsync(user.CityId.Value, cancellationToken);
            }

            var expertHomeServices = await _userService.GetByExpertIdAsync(expertId, cancellationToken);

            return new ExpertDetailsDto
            {
                ExpertId = expert.Id,
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityName = user.City?.Name ?? "نامشخص",
                ProfilePicture = user.ProfilePicture,
                Description = user.Description,
                Address = user.Address,
                ShebaNumber = user.ShebaNumber,
                CardNumber = user.CardNumber,
                Balance = user.Balance,
                Rating = expert.ExpertDetails.Rating,
                Biography = expert.ExpertDetails.Biography,
                RoleStatus = expert.ExpertDetails.RoleStatus,
                ExpertHomeServices = expertHomeServices.Select(ehs => new ExpertHomeService
                {
                    HomeServiceId = ehs.HomeServiceId,
                    HomeService = new HomeService
                    {
                        Name = ehs.HomeService.Name 
                    }
                }).ToList()

            };
        }

        public async Task<EditUserDto> GetEditUserDataAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _adminUserAppService.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                return null;
            }

            var cities = await _cityService.GetAllCitiesAsync(cancellationToken);

            return new EditUserDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityId = user.CityId,
                ProfilePicture = user.ProfilePicture,
                Description = user.Description,
                Address = user.Address,
                ShebaNumber = user.ShebaNumber,
                CardNumber = user.CardNumber,
                RoleStatus = user.RoleType == RoleEnum.Customer
                    ? (user.CustomerDetails?.RoleStatus ?? UserStatus.inActive)
                    : (user.ExpertDetails?.RoleStatus ?? UserStatus.inActive),
            };
        }

        public Task<List<ExpertHomeService>> GetHomeServiceByExpertIdAsync(int expertId, CancellationToken cancellationToken)
        {
            return _userService.GetByExpertIdAsync(expertId, cancellationToken);
        }
    }
}
