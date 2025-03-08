using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class ProposalDto
    {
        public int OrderId { get; set; }
        public List<ProposalViewItem> Proposals { get; set; }
    }
}
