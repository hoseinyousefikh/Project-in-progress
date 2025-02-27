using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Home.Entities.Other
{
    public class Comments
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        [Required(ErrorMessage = "فیلد Rating الزامی است ")]
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        public int ExpertId { get; set; }
        public Experts Experts { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public int ExpertProposalId { get; set; }
        public ExpertProposal ExpertProposal { get; set; }
    }
}
