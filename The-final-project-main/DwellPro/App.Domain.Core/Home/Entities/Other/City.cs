using App.Domain.Core.Home.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Home.Entities.Other
{
    public class City
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="فیلد نام الزامی است ")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<User> Users { get; set; }

    }

}