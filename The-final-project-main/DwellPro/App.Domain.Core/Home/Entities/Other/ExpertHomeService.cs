using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.Users;

namespace App.Domain.Core.Home.Entities.Other
{
    public class ExpertHomeService
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int ExpertId { get; set; }
        public Experts Expert { get; set; }

        public int HomeServiceId { get; set; }
        public HomeService HomeService { get; set; }
    }
}
