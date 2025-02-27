using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Other
{
    public class CommentsConfiguration : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.HasOne(c => c.Customers)
                   .WithMany(c => c.Comments)
                   .HasForeignKey(c => c.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Experts)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(c => c.ExpertId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ExpertProposal)
                   .WithOne(ep => ep.Comments)
                   .HasForeignKey<Comments>(c => c.ExpertProposalId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Comments
                {
                    Id = 1,
                    Text = "پیشنهاد خوبی است ولی نیاز به اصلاحاتی دارد.",
                    Rating = 4.5,
                    CreatedAt = DateTime.Now.AddDays(-2),
                    CustomerId = 1,
                    ExpertId = 1,
                    ExpertProposalId = 1,
                    IsDeleted = false,
                    IsApproved = true,
                },
                new Comments
                {
                    Id = 2,
                    Text = "عالی بود، همه چیز به درستی انجام شد.",
                    Rating = 5.0,
                    CreatedAt = DateTime.Now.AddDays(-1),
                    CustomerId = 2,
                    ExpertId = 2,
                    ExpertProposalId = 2,
                    IsDeleted = false,
                    IsApproved = true,
                },
                new Comments
                {
                    Id = 3,
                    Text = "کار شما خوب است ولی زمان تحویل کمی دیر بود.",
                    Rating = 3.5,
                    CreatedAt = DateTime.Now.AddDays(-3),
                    CustomerId = 3,
                    ExpertId = 3,
                    ExpertProposalId = 3,
                    IsDeleted = false,
                    IsApproved = false,
                },
                new Comments
                {
                    Id = 4,
                    Text = "پیشنهادها عالی بودند، فقط نیاز به هماهنگی بیشتر با تیم داشتیم.",
                    Rating = 4.0,
                    CreatedAt = DateTime.Now.AddDays(-5),
                    CustomerId = 2,
                    ExpertId = 2,
                    ExpertProposalId = 4,
                    IsDeleted = false,
                    IsApproved = true,
                },
                new Comments
                {
                    Id = 5,
                    Text = "کار خیلی خوب و سریع انجام شد، از همکاری با شما راضی هستم.",
                    Rating = 5.0,
                    CreatedAt = DateTime.Now.AddDays(-4),
                    CustomerId = 3,
                    ExpertId = 3,
                    ExpertProposalId = 5,
                    IsDeleted = false,
                    IsApproved = true,
                }
            );
        }
    }
}
