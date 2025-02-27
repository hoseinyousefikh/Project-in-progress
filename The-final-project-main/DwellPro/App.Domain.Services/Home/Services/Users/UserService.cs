using App.Domain.Core.Home.Contract.Repositories.Users;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ICustomersRepository _customersRepository;
    private readonly IExpertRepository _expertRepository;
    private readonly SignInManager<User> _signInManager;


    public UserService(UserManager<User> userManager,
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

 
    public async Task<IdentityResult> RegisterAsync( string firstName, string lastName,  string email,  string password,  string confirmPassword,  int cityId,  RoleEnum roleType, CancellationToken cancellationToken)
    {
        if (roleType == RoleEnum.Admin)
        {
            throw new Exception("Admin registration is not allowed.");
        }

        if (password != confirmPassword)
        {
            throw new Exception("Password and Confirm Password do not match.");
        }

        int roleId = roleType == RoleEnum.Customer ? 2 : (roleType == RoleEnum.Expert ? 3 : 0);
        if (roleId == 0)
        {
            throw new Exception("Invalid role type.");
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = email,
            Email = email,
            RoleType = roleType,
            CityId = cityId,
            RegisterAt = DateTime.UtcNow,
            IsDeleted = false,
            RoleId = roleId 
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            return result;
        }

        var role = await _roleManager.FindByNameAsync(roleType.ToString());
        if (role == null)
        {
            role = new IdentityRole<int>
            {
                Id = roleId,
                Name = roleType.ToString(),
                NormalizedName = roleType.ToString().ToUpper()
            };
            await _roleManager.CreateAsync(role);
        }

        await _userManager.AddToRoleAsync(user, role.Name);

      
        if (roleType == RoleEnum.Customer)
        {
            var customer = new Customers
            {
                UserId = user.Id,
                RoleStatus = UserStatus.inActive,
                IsDeleted = false
            };
            var customerAdded = await _customersRepository.AddAsync(customer, cancellationToken);
            if (!customerAdded)
            {
                throw new Exception("Error adding customer details.");
            }
        }
        else if (roleType == RoleEnum.Expert)
        {
            var expert = new Experts
            {
                UserId = user.Id,
                RoleStatus = UserStatus.inActive,
                IsDeleted = false
            };
            var expertAdded = await _expertRepository.AddAsync(expert, cancellationToken);
            if (!expertAdded)
            {
                throw new Exception("Error adding expert details.");
            }
        }

        return result;
    }

    public async Task<IdentityResult> Login(string username, string password, bool rememberMe)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Invalid login attempt." });
        }

        if (user.RoleType == RoleEnum.Admin)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Admin users are not allowed to log in." });
        }

        var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, false);

        return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();
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
     UserStatus roleStatus, 
     CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new Exception("کاربر یافت نشد.");
        }

        if (user.RoleType == RoleEnum.Admin)
        {
            throw new Exception("ادمین مجاز به بروزرسانی این اطلاعات نیست.");
        }

        user.FirstName = firstName;
        user.LastName = lastName;
        if (cityId.HasValue)
        {
            user.CityId = cityId.Value;
        }
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

        if (user.RoleType == RoleEnum.Customer)
        {
            var customer = await _customersRepository.GetByIdAsync(user.Id, cancellationToken);
            if (customer != null)
            {
                customer.RoleStatus = roleStatus;
                var customerUpdated = await _customersRepository.UpdateAsync(customer, cancellationToken);
                if (!customerUpdated)
                {
                    throw new Exception("خطا در بروزرسانی اطلاعات مشتری.");
                }
            }
        }
        else if (user.RoleType == RoleEnum.Expert)
        {
            var expert = await _expertRepository.GetByIdAsync(user.Id, cancellationToken);
            if (expert != null)
            {
                expert.RoleStatus = roleStatus; 
                var expertUpdated = await _expertRepository.UpdateAsync(expert, cancellationToken);
                if (!expertUpdated)
                {
                    throw new Exception("خطا در بروزرسانی اطلاعات کارشناس.");
                }
            }
        }

        return IdentityResult.Success;
    }


    public async Task<IdentityResult> UpdatePasswordAsync(int userId, string newPassword, string confirmPassword, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        if (user.RoleType == RoleEnum.Admin)
        {
            throw new Exception("Admin users are not allowed to update their password.");
        }

        if (newPassword != confirmPassword)
        {
            throw new Exception("Password and Confirm Password do not match.");
        }

        var passwordValidationResult = await _userManager.ChangePasswordAsync(user, user.PasswordHash, newPassword);
        if (!passwordValidationResult.Succeeded)
        {
            return passwordValidationResult;
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateEmailAsync(int userId, string newEmail, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        if (user.RoleType == RoleEnum.Admin)
        {
            throw new Exception("Admin users are not allowed to update their email.");
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


    public async Task<IdentityResult> DeleteUserAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new Exception("کاربر یافت نشد.");
        }

        if (user.RoleType == RoleEnum.Admin)
        {
            throw new Exception("ادمین مجاز به حذف نیست.");
        }

        if (user.RoleType == RoleEnum.Customer)
        {
            var customer = await _customersRepository.GetByIdAsync(user.Id, cancellationToken);
            if (customer != null)
            {
                var customerDeleted = await _customersRepository.DeleteAsync(customer.Id, cancellationToken);
                if (!customerDeleted)
                {
                    throw new Exception("خطا در حذف اطلاعات مشتری.");
                }
            }
        }
        else if (user.RoleType == RoleEnum.Expert)
        {
            var expert = await _expertRepository.GetByIdAsync(user.Id, cancellationToken);
            if (expert != null)
            {
                var expertDeleted = await _expertRepository.DeleteAsync(expert.Id, cancellationToken);
                if (!expertDeleted)
                {
                    throw new Exception("خطا در حذف اطلاعات کارشناس.");
                }
            }
        }

        user.IsDeleted = true;  

        var result = await _userManager.UpdateAsync(user);  
        if (!result.Succeeded)
        {
            return result;
        }


        return IdentityResult.Success;
    }


}
