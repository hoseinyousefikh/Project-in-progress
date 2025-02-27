using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "فیلد نام الزامی است ")]
        public string Name { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
