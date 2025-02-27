using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Enum;

namespace App.Domain.Core.Home.Entities.Users
{
    public class Customers
    {
        public int Id { get; set; }
        public UserStatus RoleStatus { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int UserId { get; set; }  
        public User User { get; set; }
        public List<Orders> Orders { get; set; }
        public List<Comments> Comments { get; set; }

    }
}
