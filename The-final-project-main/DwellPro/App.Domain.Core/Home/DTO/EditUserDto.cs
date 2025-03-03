using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    using App.Domain.Core.Home.Enum;
    using System.ComponentModel.DataAnnotations;

    public class EditUserDto
    {
        [Required]
        public int UserId { get; set; }

        [StringLength(50, ErrorMessage = "نام کوچک نباید بیشتر از 50 کاراکتر باشد")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "نام خانوادگی نباید بیشتر از 50 کاراکتر باشد")]
        public string? LastName { get; set; }

        public int? CityId { get; set; }

        public string? ProfilePicture { get; set; }

        [StringLength(500, ErrorMessage = "توضیحات نباید بیشتر از 500 کاراکتر باشد")]
        public string? Description { get; set; }

        [StringLength(200, ErrorMessage = "آدرس نباید بیشتر از 200 کاراکتر باشد")]
        public string? Address { get; set; }

        public string? ShebaNumber { get; set; }

        public string? CardNumber { get; set; }

        [Required]
        public UserStatus RoleStatus { get; set; }
    }

}
