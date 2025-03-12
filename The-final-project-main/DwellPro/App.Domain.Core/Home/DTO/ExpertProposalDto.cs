using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class ExpertProposalDto
    {
        public int OrderId { get; set; }
        public string ServiceTitle { get; set; }
        public DateTime OrderDate { get; set; }
        public int ExpertId { get; set; }


        public decimal ProposedPrice { get; set; }
        public string ProposalDescription { get; set; }
        public DateTime? ProposalDate { get; set; }
        public TimeSpan? ProposedExecutionTime { get; set; }

        public DateTime WorkCompletionDate { get; set; }
        public DateTime? CustomerSelectionDate { get; set; } 
    }
}
