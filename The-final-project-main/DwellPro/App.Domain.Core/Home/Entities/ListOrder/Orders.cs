using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.Home.Entities.ListOrder
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExecutionDate { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? BasePrice { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public bool IsApproved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public List<ExpertProposal> ExpertProposals { get; set; }
        public int HomeServiceId { get; set; }
        public HomeService HomeServiceName { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
        public List<Pictures> Pictures { get; set; } 
    }
}
