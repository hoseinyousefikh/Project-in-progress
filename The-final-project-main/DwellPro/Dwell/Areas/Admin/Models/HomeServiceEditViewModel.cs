using App.Domain.Core.Home.Entities.Categories;
using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Areas.Admin.Models
{
    public class HomeServiceEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام سرویس الزامی است.")]
        [StringLength(100, ErrorMessage = "نام سرویس نباید بیش از 100 کاراکتر باشد.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "توضیحات الزامی است.")]
        [StringLength(500, ErrorMessage = "توضیحات نباید بیش از 500 کاراکتر باشد.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "قیمت پایه الزامی است.")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت پایه باید یک عدد مثبت باشد.")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "لطفاً دسته‌بندی فرعی را انتخاب کنید.")]
        public int SubCategoryId { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "تعداد مشاهده الزامی است.")]
        [Range(0, int.MaxValue, ErrorMessage = "تعداد مشاهده باید یک عدد مثبت باشد.")]
        public int ViewCount { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }

        public List<SubCategory>? SubCategories { get; set; }
    }
}
