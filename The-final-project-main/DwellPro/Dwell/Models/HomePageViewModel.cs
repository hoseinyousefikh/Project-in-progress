using App.Domain.Core.Home.Entities.Categories;

namespace DwellMVC.Models
{
    public class HomePageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<HomeService> TopHomeServices { get; set; }
        public List<HomeService> LatestHomeServices { get; set; } 

    }

}
