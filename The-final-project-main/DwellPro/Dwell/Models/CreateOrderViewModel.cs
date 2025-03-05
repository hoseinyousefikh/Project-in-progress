using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Models
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "لطفاً سرویس مورد نظر را انتخاب کنید.")]
        public int HomeServiceId { get; set; }

        public List<HomeService>? HomeServices { get; set; }

        [Required(ErrorMessage = "تاریخ انجام کار الزامی است.")]
        public DateTime ExecutionDate { get; set; }

        [Required(ErrorMessage = "زمان انجام کار الزامی است.")]
        [DataType(DataType.Time)] 
        public TimeSpan ExecutionTime { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "قیمت پایه الزامی است.")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت پایه باید یک عدد مثبت باشد.")]
        public decimal? BasePrice { get; set; }

        public List<Customers>? Users { get; set; }
        public HomeService? SelectedHomeService { get; set; }
        public List<IFormFile>? Pictures { get; set; }
        public List<string>? ExistingPictures { get; set; }

    }
}
