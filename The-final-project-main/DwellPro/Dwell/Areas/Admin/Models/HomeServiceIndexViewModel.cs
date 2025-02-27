using App.Domain.Core.Home.Entities.Categories;
using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Areas.Admin.Models
{
    public class HomeServiceIndexViewModel
    {
        [Required(ErrorMessage = "لیست زیر دسته‌ها باید مشخص شود.")]
        public List<SubCategory> SubCategories { get; set; }

        [Required(ErrorMessage = "لیست خدمات خانگی باید مشخص شود.")]
        public List<HomeService> HomeServices { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک زیر دسته معتبر انتخاب کنید.")]
        public int? SelectedSubCategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "صفحه جاری باید یک عدد مثبت باشد.")]
        public int CurrentPage { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "تعداد صفحات باید یک عدد مثبت باشد.")]
        public int TotalPages { get; set; }
    }

}
