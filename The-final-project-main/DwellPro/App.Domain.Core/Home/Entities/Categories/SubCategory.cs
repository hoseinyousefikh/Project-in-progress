using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Home.Entities.Categories
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "فیلد نام الزامی است ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "فیلد عکس الزامی است ")]
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;


        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<HomeService> HomeServices { get; set; }
    }

}
