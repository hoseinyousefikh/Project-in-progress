using App.Domain.Core.Home.Entities.Categories;
using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Areas.Admin.Models
{
    public class HomeServiceCreateViewModel
    {
        [Required(ErrorMessage = "لطفاً نام سرویس را وارد کنید.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفاً توضیحات سرویس را وارد کنید.")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "قیمت پایه نمی‌تواند منفی باشد.")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "لطفاً دسته‌بندی فرعی را انتخاب کنید.")]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "لطفاً یک تصویر انتخاب کنید.")]
        public IFormFile ImageFile { get; set; }

        public List<SubCategory>? SubCategories { get; set; }
    }

}
