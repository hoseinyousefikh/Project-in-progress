using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Enum
{
    public enum PaymentStatus
    {
        [Display(Name = "پرداخت نشده")]
        Pending = 1,

        [Display(Name = "پرداخت شده")]
        Paid = 2
    }

}
