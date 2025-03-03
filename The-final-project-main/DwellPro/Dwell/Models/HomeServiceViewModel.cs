namespace DwellMVC.Models
{
    public class HomeServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public int ViewCount { get; set; }
        public int SubCategoryId { get; set; }
    }
}
