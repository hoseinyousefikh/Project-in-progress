using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Users;

namespace DwellMVC.Models
{
    public class ProposalViewModel
    {
        public int OrderId { get; set; }
        public List<ProposalViewItem> Proposals { get; set; }
    }

    public class ProposalViewItem
    {
        public ExpertProposal Proposal { get; set; }
        public Experts Expert { get; set; }
    }


}
