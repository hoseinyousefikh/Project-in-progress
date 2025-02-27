using App.Domain.Core.Home.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Users
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Roles");
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();

            builder.HasData(new List<Role>()
            {
                new Role() {Id = 1 ,Title = "Admin"},
                new Role() {Id = 2 ,Title = "Visitor"},
                new Role() {Id = 3 ,Title = "Advertiser"},

            });

        }
    }
}
