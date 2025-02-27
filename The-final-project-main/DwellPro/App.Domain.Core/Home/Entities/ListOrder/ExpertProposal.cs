using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.Home.Entities.ListOrder
{
    public class ExpertProposal
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsApproved { get; set; } = false;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProposedPrice { get; set; }
        public string? ProposalDescription { get; set; }

        public DateTime? ProposalDate { get; set; }
        public DateTime WorkCompletionDate { get; set; }
        public TimeSpan? ProposedExecutionTime { get; set; }
        public DateTime? CustomerSelectionDate { get; set; }


        public ProposalStatus ProposalStatus { get; set; }
        public bool IsSelectedByCustomer { get; set; } = false;

        public int OrderId { get; set; }
        public Orders Order { get; set; }

        public int ExpertId { get; set; }
        public Experts Expert { get; set; }
        public int? CommentId { get; set; }
        public Comments? Comments { get; set; }

    }
}
