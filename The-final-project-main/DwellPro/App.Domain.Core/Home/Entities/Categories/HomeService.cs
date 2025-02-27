using App.Domain.Core.Home.Entities.Other;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.Home.Entities.Categories
{
    public class HomeService
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "فیلد نام الزامی است ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "فیلد عکس الزامی است ")]
        public string ImageUrl { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }
        public int ViewCount { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<ExpertHomeService> ExpertHomeServices { get; set; } 

    }

}
