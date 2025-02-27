using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Enum
{
    public enum OrderStatus
    {
        [Display(Name = "منتظر انتخاب متخصص")]
        WaitingForExpertSelection = 1,

        [Display(Name = "منتظر پیشنهاد متخصص")]
        WaitingForExpertProposal = 2,

        [Display(Name = "منتظر آمدن متخصص به محل")]
        WaitingForExpertArrival = 3,

        [Display(Name = "اتمام کار")]
        Completed = 4
    }


}
