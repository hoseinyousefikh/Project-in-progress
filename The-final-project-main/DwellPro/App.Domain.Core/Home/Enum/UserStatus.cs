﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Enum
{
    public enum UserStatus
    {
        [Display(Name ="فعال")]
        Active = 1,

        [Display(Name = "غیر فعال")]
        inActive = 2
    }
}
