using App.Domain.Core.Home.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "فیلد نام الزامی است ")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<User> Users { get; set; }

    }
}
