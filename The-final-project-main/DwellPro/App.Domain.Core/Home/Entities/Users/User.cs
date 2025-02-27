using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.Home.Entities.Users
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Balance { get; set; }
        public string? Address { get; set; }
        public string? ShebaNumber { get; set; }
        public string? CardNumber { get; set; }
        public DateTime RegisterAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Description { get; set; }

        public RoleEnum RoleType { get; set; }
        public int RoleId { get; set; }

        public int? CityId { get; set; }
        public City? City { get; set; }
        public Experts? ExpertDetails { get; set; }
        public Customers? CustomerDetails { get; set; }
    }
}
