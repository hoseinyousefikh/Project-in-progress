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
    public class PicturesConfiguration : IEntityTypeConfiguration<Pictures>
    {
        public void Configure(EntityTypeBuilder<Pictures> builder)
        {
            builder.HasOne(p => p.Orders)
                   .WithMany(c => c.Pictures)
                   .HasForeignKey(p => p.OrdersId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
