using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using App.Domain.Core.Home.Entities.ListOrder;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Users
{
    public class CustomersConfiguration : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> builder)
        {
            builder.HasMany(c => c.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Comments)
                   .WithOne(cm => cm.Customers)
                   .HasForeignKey(cm => cm.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
               new Customers
               {
                   Id = 1,
                   UserId = 5, 
                   IsDeleted = false,
                   RoleStatus = UserStatus.inActive
               },
               new Customers
               {
                   Id = 2,
                   UserId = 6,
                   IsDeleted = false,
                   RoleStatus = UserStatus.inActive
               },
               new Customers
               {
                   Id = 3,
                   UserId = 7,
                   IsDeleted = false,
                   RoleStatus = UserStatus.inActive
               }
           );
        }
    }
}
