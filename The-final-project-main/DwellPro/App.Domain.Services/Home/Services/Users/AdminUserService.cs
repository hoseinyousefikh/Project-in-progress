using App.Domain.Core.Home.Contract.Repositories.Users;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.Users
{
    public class AdminUserService : IAdminUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ICustomersRepository _customersRepository;
        private readonly IExpertRepository _expertRepository;
        private readonly SignInManager<User> _signInManager;


        public AdminUserService(UserManager<User> userManager,
                            RoleManager<IdentityRole<int>> roleManager,
                            IPasswordHasher<User> passwordHasher,
                            ICustomersRepository customersRepository,
                            IExpertRepository expertRepository,
                            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _customersRepository = customersRepository;
            _expertRepository = expertRepository;
            _signInManager = signInManager;

        }

        public async Task<IdentityResult> LoginAdminAsync(string username, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid login attempt." });
            }

            if (user.RoleType != RoleEnum.Admin)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Only Admin users are allowed to log in." });
            }

            var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, false);

            return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAdminAsync(
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
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("کاربر پیدا نشد.");
            }

            if (user.RoleType != RoleEnum.Admin)
            {
                throw new Exception("فقط کاربران ادمین می‌توانند پروفایل خود را به روز رسانی کنند.");
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            user.CityId = cityId.HasValue ? cityId.Value : 0;  

            user.ProfilePicture = profilePicture;
            user.Description = description;
            user.Address = address;
            user.ShebaNumber = shebaNumber;
            user.CardNumber = cardNumber;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return updateResult;
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAdminPasswordAsync(int userId, string currentPassword, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.RoleType != RoleEnum.Admin)
            {
                throw new Exception("Only Admin users can update their password.");
            }

         

            var passwordValidationResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!passwordValidationResult.Succeeded)
            {
                return passwordValidationResult;
            }

            return IdentityResult.Success;
        }


        public async Task<IdentityResult> UpdateAdminEmailAsync(int userId, string newEmail, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.RoleType != RoleEnum.Admin)
            {
                throw new Exception("Only Admin users can update their email.");
            }

            user.Email = newEmail;
            user.UserName = newEmail;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return updateResult;
            }

            return IdentityResult.Success;
        }

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                                          .Where(u => !u.IsDeleted)
                                          .ToListAsync(cancellationToken);

            foreach (var user in users)
            {
                if (user.RoleType == RoleEnum.Customer)
                {
                    var customer = await _customersRepository.GetByIdAsync(user.Id, cancellationToken);
                    if (customer != null && !customer.IsDeleted)
                    {
                        user.CustomerDetails = customer;
                    }
                }
                else if (user.RoleType == RoleEnum.Expert)
                {
                    var expert = await _expertRepository.GetByIdAsync(user.Id, cancellationToken);
                    if (expert != null && !expert.IsDeleted)
                    {
                        user.ExpertDetails = expert;
                    }
                }
            }

            return users;
        }


        public async Task<User> GetByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.RoleType == RoleEnum.Customer)
            {
                var customer = await _customersRepository.GetByIdAsync(userId, cancellationToken);
                user.CustomerDetails = customer;
            }
            else if (user.RoleType == RoleEnum.Expert)
            {
                var expert = await _expertRepository.GetByIdAsync(userId, cancellationToken);
                user.ExpertDetails = expert;
            }

            return user;
        }

    }
}
