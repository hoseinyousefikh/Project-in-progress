using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Home.DTO
{
    public class UpdateEmailDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "New email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string NewEmail { get; set; }
    }
}
