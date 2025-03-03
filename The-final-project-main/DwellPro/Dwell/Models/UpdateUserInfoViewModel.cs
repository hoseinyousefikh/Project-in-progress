using App.Domain.Core.Home.DTO;

namespace DwellMVC.Models
{
    public class UpdateUserInfoViewModel
    {
        public UpdatePasswordDto UpdatePassword { get; set; }
        public UpdateEmailDto UpdateEmail { get; set; }
    }

}
