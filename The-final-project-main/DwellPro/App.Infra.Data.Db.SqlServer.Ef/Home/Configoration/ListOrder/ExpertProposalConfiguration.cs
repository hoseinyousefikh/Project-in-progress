using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.ListOrder
{
    public class ExpertProposalConfiguration : IEntityTypeConfiguration<ExpertProposal>
    {
        public void Configure(EntityTypeBuilder<ExpertProposal> builder)
        {
            builder.HasOne(ep => ep.Expert)
                   .WithMany(e => e.ExpertProposals)
                   .HasForeignKey(ep => ep.ExpertId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ep => ep.Order)
                   .WithMany(o => o.ExpertProposals)
                   .HasForeignKey(ep => ep.OrderId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ep => ep.Comments)
                  .WithOne(c => c.ExpertProposal)
                  .HasForeignKey<ExpertProposal>(ep => ep.CommentId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new ExpertProposal
                {
                    Id = 1,
                    IsDeleted = false,
                    IsApproved = true,
                    ProposedPrice = 2000.00m,
                    ProposalDescription = "پیشنهاد انجام پروژه طراحی وب سایت",
                    ProposalDate = DateTime.Now.AddDays(-5),
                    WorkCompletionDate = DateTime.Now.AddMonths(2),
                    ProposedExecutionTime = TimeSpan.FromDays(60),
                    CustomerSelectionDate = DateTime.Now.AddDays(1),
                    ProposalStatus = ProposalStatus.Pending,
                    IsSelectedByCustomer = false,
                    OrderId = 1,
                    ExpertId = 1,
                    CommentId = 1,
                },
                new ExpertProposal
                {
                    Id = 2,
                    IsDeleted = false,
                    IsApproved = false,
                    ProposedPrice = 1500.00m,
                    ProposalDescription = "پیشنهاد انجام پروژه طراحی اپلیکیشن موبایل",
                    ProposalDate = DateTime.Now.AddDays(-4),
                    WorkCompletionDate = DateTime.Now.AddMonths(1),
                    ProposedExecutionTime = TimeSpan.FromDays(30),
                    CustomerSelectionDate = DateTime.Now.AddDays(2),
                    ProposalStatus = ProposalStatus.Rejected,
                    IsSelectedByCustomer = false,
                    OrderId = 2,
                    ExpertId = 2,
                    CommentId = 2,
                },
                new ExpertProposal
                {
                    Id = 3,
                    IsDeleted = false,
                    IsApproved = true,
                    ProposedPrice = 2500.00m,
                    ProposalDescription = "پیشنهاد انجام پروژه ساخت وب سایت فروشگاهی",
                    ProposalDate = DateTime.Now.AddDays(-3),
                    WorkCompletionDate = DateTime.Now.AddMonths(3),
                    ProposedExecutionTime = TimeSpan.FromDays(90),
                    CustomerSelectionDate = DateTime.Now.AddDays(3),
                    ProposalStatus = ProposalStatus.Pending,
                    IsSelectedByCustomer = true,
                    OrderId = 3,
                    ExpertId = 3,
                    CommentId = 3,
                },
                new ExpertProposal
                {
                    Id = 4,
                    IsDeleted = false,
                    IsApproved = false,
                    ProposedPrice = 1800.00m,
                    ProposalDescription = "پیشنهاد انجام پروژه طراحی لوگو",
                    ProposalDate = DateTime.Now.AddDays(-2),
                    WorkCompletionDate = DateTime.Now.AddMonths(1),
                    ProposedExecutionTime = TimeSpan.FromDays(15),
                    CustomerSelectionDate = DateTime.Now.AddDays(5),
                    ProposalStatus = ProposalStatus.Pending,
                    IsSelectedByCustomer = false,
                    OrderId = 4,
                    ExpertId = 2,
                    CommentId = 4,
                },
                new ExpertProposal
                {
                    Id = 5,
                    IsDeleted = false,
                    IsApproved = true,
                    ProposedPrice = 2200.00m,
                    ProposalDescription = "پیشنهاد انجام پروژه طراحی گرافیکی",
                    ProposalDate = DateTime.Now.AddDays(-1),
                    WorkCompletionDate = DateTime.Now.AddMonths(1),
                    ProposedExecutionTime = TimeSpan.FromDays(45),
                    CustomerSelectionDate = DateTime.Now.AddDays(4),
                    ProposalStatus = ProposalStatus.Pending,
                    IsSelectedByCustomer = false,
                    OrderId = 5,
                    ExpertId = 3,
                    CommentId = 5,
                }
               
            );
        }
    }
}
