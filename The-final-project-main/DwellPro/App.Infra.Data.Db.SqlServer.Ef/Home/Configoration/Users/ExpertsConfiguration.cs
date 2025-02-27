using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Users
{
    public class ExpertsConfiguration : IEntityTypeConfiguration<Experts>
    {
        public void Configure(EntityTypeBuilder<Experts> builder)
        {
            builder.HasMany(e => e.ExpertProposals)
                .WithOne(ep => ep.Expert)
                .HasForeignKey(ep => ep.ExpertId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ExpertHomeServices)
                .WithOne(ehs => ehs.Expert)
                .HasForeignKey(ehs => ehs.ExpertId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                 new Experts
                 {
                     Id = 1,
                     UserId = 2, 
                     IsDeleted = false,
                     RoleStatus = UserStatus.inActive, 
                     Biography = "من بهترین کارشناس هستم و توانمندی‌های زیادی دارم.",
                     Rating = 4.5
                 },
                 new Experts
                 {
                     Id = 2,
                     UserId = 3,
                     IsDeleted = false,
                     RoleStatus = UserStatus.inActive,
                     Biography = "کارشناس عالی در زمینه طراحی و برنامه‌نویسی.",
                     Rating = 4.7
                 },
                 new Experts
                 {
                     Id = 3,
                     UserId = 4,
                     IsDeleted = false,
                     RoleStatus = UserStatus.inActive,
                     Biography = "من در مشاوره و برنامه‌ریزی کسب‌وکار تخصص دارم.",
                     Rating = 4.8
                 }
             );
        }
    }
}
