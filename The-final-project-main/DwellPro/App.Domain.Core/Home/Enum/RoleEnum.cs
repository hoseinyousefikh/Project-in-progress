using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Enum
{
    public enum RoleEnum
    {
        [Display(Name = "ادمین")]
        Admin = 1,

        [Display(Name = "مشتری")]
        Customer = 2,

        [Display(Name = "کارشناس")]
        Expert = 3
    }
}
