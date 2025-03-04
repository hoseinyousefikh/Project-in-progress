namespace DwellMVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ServiceTitle { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
