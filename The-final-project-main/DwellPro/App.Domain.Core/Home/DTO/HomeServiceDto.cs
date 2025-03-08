using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class HomeServiceDto
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
