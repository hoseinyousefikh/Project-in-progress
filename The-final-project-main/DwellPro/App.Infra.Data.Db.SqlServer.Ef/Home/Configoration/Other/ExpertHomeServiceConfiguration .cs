using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Home.Entities.Other;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Other
{
    public class ExpertHomeServiceConfiguration : IEntityTypeConfiguration<ExpertHomeService>
    {
        public void Configure(EntityTypeBuilder<ExpertHomeService> builder)
        {
            builder.HasKey(eh => eh.Id);

            builder.HasOne(eh => eh.Expert)
                   .WithMany(e => e.ExpertHomeServices)
                   .HasForeignKey(eh => eh.ExpertId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(eh => eh.HomeService)
                   .WithMany(hs => hs.ExpertHomeServices)
                   .HasForeignKey(eh => eh.HomeServiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
