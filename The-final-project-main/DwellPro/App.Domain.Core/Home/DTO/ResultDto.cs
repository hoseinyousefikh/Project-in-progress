using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class ResultDto
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int? OrderId { get; set; } 

    }


}
