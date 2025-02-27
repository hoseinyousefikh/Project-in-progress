using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "فیلد نام الزامی است ")]
        public string Name { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
