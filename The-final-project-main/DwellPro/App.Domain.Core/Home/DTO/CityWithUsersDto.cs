using App.Domain.Core.Home.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class CityWithUsersDto
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<User> Users { get; set; }
    }
}
