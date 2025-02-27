using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Areas.Admin.Models
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام زیرمجموعه الزامی است.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است.")]
        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

}
