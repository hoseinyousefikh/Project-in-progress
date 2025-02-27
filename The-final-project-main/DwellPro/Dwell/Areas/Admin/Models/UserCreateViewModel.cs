using App.Domain.Core.Home.Enum;
using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Areas.Admin.Models
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "نام ضروری است.")]
        [MaxLength(50, ErrorMessage = "نام نباید بیشتر از 50 کاراکتر باشد.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی ضروری است.")]
        [MaxLength(50, ErrorMessage = "نام خانوادگی نباید بیشتر از 50 کاراکتر باشد.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ایمیل ضروری است.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد کنید.")]
        [MaxLength(100, ErrorMessage = "ایمیل نباید بیشتر از 100 کاراکتر باشد.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور ضروری است.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تأیید رمز عبور ضروری است.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تأیید آن مطابقت ندارند.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "شهر ضروری است.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "نوع نقش ضروری است.")]
        public RoleEnum RoleType { get; set; }
    }


}
