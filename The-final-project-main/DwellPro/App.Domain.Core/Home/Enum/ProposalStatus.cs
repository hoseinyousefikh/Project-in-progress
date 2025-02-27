using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Home.Enum
{
    public enum ProposalStatus
    {
        [Display(Name = "در حال بررسی")]
        Pending = 1,

        [Display(Name = "پذیرفته شده")]
        Accepted = 2,

        [Display(Name = "رد شده")]
        Rejected = 3
    }

}
