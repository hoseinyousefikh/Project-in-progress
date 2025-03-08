using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.DTO
{
    public class ProposalViewItem
    {
        public ExpertProposal Proposal { get; set; }
        public Experts Expert { get; set; }
    }
}
