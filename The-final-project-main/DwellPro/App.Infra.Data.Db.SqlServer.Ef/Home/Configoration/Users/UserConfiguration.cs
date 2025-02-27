using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Core.Home.Enum;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using App.Domain.Core.Home.Entities.ListOrder;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(100);
            builder.Property(u => u.LastName).HasMaxLength(100);
            builder.Property(u => u.ProfilePicture).HasMaxLength(255);
            builder.Property(u => u.RegisterAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(u => u.City)
                .WithMany()
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.ExpertDetails)
                .WithOne(e => e.User)
                .HasForeignKey<Experts>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.CustomerDetails)
                .WithOne(c => c.User)
                .HasForeignKey<Customers>(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            var hasher = new PasswordHasher<User>();

            var admin = new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "حسین",
                LastName = "یوسفی",
                Balance = 1000000m,
                RoleType = RoleEnum.Admin,
                RegisterAt = DateTime.Now,
                RoleId = 1,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash = hasher.HashPassword(admin, "H1h2h3h4@");

            var expert1 = new User
            {
                Id = 2,
                UserName = "expert1",
                NormalizedUserName = "EXPERT1",
                Email = "expert1@example.com",
                NormalizedEmail = "EXPERT1@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "کارشناس",
                LastName = "یک",
                Balance = 500000m,
                RoleType = RoleEnum.Expert,
                RegisterAt = DateTime.Now,
                RoleId = 3,
                SecurityStamp = Guid.NewGuid().ToString()

            };
            expert1.PasswordHash = hasher.HashPassword(expert1, "Expert1@123");

            var expert2 = new User
            {
                Id = 3,
                UserName = "expert2",
                NormalizedUserName = "EXPERT2",
                Email = "expert2@example.com",
                NormalizedEmail = "EXPERT2@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "کارشناس",
                LastName = "دو",
                Balance = 600000m,
                RoleType = RoleEnum.Expert,
                RegisterAt = DateTime.Now,
                RoleId = 3,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            expert2.PasswordHash = hasher.HashPassword(expert2, "Expert2@123");

            var expert3 = new User
            {
                Id = 4,
                UserName = "expert3",
                NormalizedUserName = "EXPERT3",
                Email = "expert3@example.com",
                NormalizedEmail = "EXPERT3@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "کارشناس",
                LastName = "سه",
                Balance = 700000m,
                RoleType = RoleEnum.Expert,
                RegisterAt = DateTime.Now,
                RoleId = 3,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            expert3.PasswordHash = hasher.HashPassword(expert3, "Expert3@123");

            var customer1 = new User
            {
                Id = 5,
                UserName = "customer1",
                NormalizedUserName = "CUSTOMER1",
                Email = "customer1@example.com",
                NormalizedEmail = "CUSTOMER1@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "مشتری",
                LastName = "یک",
                Balance = 200000m,
                RoleType = RoleEnum.Customer,
                RegisterAt = DateTime.Now,
                RoleId = 2,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            customer1.PasswordHash = hasher.HashPassword(customer1, "Customer1@123");

            var customer2 = new User
            {
                Id = 6,
                UserName = "customer2",
                NormalizedUserName = "CUSTOMER2",
                Email = "customer2@example.com",
                NormalizedEmail = "CUSTOMER2@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "مشتری",
                LastName = "دو",
                Balance = 300000m,
                RoleType = RoleEnum.Customer,
                RegisterAt = DateTime.Now,
                RoleId = 2,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            customer2.PasswordHash = hasher.HashPassword(customer2, "Customer2@123");

            var customer3 = new User
            {
                Id = 7,
                UserName = "customer3",
                NormalizedUserName = "CUSTOMER3",
                Email = "customer3@example.com",
                NormalizedEmail = "CUSTOMER3@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "مشتری",
                LastName = "سه",
                Balance = 400000m,
                RoleType = RoleEnum.Customer,
                RegisterAt = DateTime.Now,
                RoleId = 2,
                SecurityStamp = Guid.NewGuid().ToString(),
               
            };

            customer3.PasswordHash = hasher.HashPassword(customer3, "Customer3@123");
           
            builder.HasData(customer3,admin, expert1, expert2, expert3, customer1, customer2);
           
           
        }
    }
}
//