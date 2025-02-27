using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Home.Entities.Other
{
    public class Pictures
    {
        public int Id { get; set; }

        public int OrdersId { get; set; }
        public Orders Orders { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Required(ErrorMessage = "فیلد عکس الزامی است ")]
        public string ImageUrl { get; set; }

        public DateTime UploadedAt { get; set; }
        
    }
}
