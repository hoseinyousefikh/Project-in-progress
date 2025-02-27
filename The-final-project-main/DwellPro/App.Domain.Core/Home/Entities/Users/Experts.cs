using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Home.Entities.Users
{
    public class Experts 
    {
        public int Id { get; set; }
        public UserStatus RoleStatus { get; set; }

        [Required(ErrorMessage = "فیلد Rating الزامی است ")]
        public double Rating { get; set; }
        public string? Biography { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int UserId { get; set; }  
        public User User { get; set; }
        public List<ExpertProposal> ExpertProposals { get; set; }
        public List<ExpertHomeService> ExpertHomeServices { get; set; }
        public List<Orders> Orders { get; set; }
        public List<HomeService> HomeServices { get; set; }
        public List<Comments> Comments { get; set; }

    }
}
