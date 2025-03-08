using App.Domain.Core.Home.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class UserDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CityName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? ShebaNumber { get; set; }
        public string? CardNumber { get; set; }
        public decimal? Balance { get; set; }
        public UserStatus RoleStatus { get; set; }
    }

}
