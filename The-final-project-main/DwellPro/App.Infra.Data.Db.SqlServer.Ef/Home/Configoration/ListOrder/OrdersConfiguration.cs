using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.ListOrder
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasMany(o => o.ExpertProposals)
                   .WithOne(ep => ep.Order)
                   .HasForeignKey(ep => ep.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Pictures)
                   .WithOne(p => p.Orders)
                   .HasForeignKey(p => p.OrdersId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Orders
                {
                    Id = 1,
                    RequestDate = DateTime.Now.AddDays(-3),
                    ExecutionDate = DateTime.Now.AddDays(7),
                    ExecutionTime = TimeSpan.FromDays(5),
                    Description = "پروژه برای طراحی و توسعه وب سایت",
                    BasePrice = 1500.00m,
                    OrderStatus = OrderStatus.WaitingForExpertSelection, 
                    PaymentStatus = PaymentStatus.Pending,
                    IsApproved = true,
                    IsDeleted = false,
                    HomeServiceId = 1,
                    CustomerId = 1
                },
                new Orders
                {
                    Id = 2,
                    RequestDate = DateTime.Now.AddDays(-2),
                    ExecutionDate = DateTime.Now.AddDays(10),
                    ExecutionTime = TimeSpan.FromDays(7),
                    Description = "پروژه طراحی اپلیکیشن موبایل",
                    BasePrice = 2000.00m,
                    OrderStatus = OrderStatus.WaitingForExpertProposal, 
                    PaymentStatus = PaymentStatus.Paid,
                    IsApproved = true,
                    IsDeleted = false,
                    HomeServiceId = 2,
                    CustomerId = 2
                },
                new Orders
                {
                    Id = 3,
                    RequestDate = DateTime.Now.AddDays(-1),
                    ExecutionDate = DateTime.Now.AddDays(15),
                    ExecutionTime = TimeSpan.FromDays(10),
                    Description = "پروژه طراحی سیستم مدیریت محتوا",
                    BasePrice = 2500.00m,
                    OrderStatus = OrderStatus.WaitingForExpertArrival, 
                    PaymentStatus = PaymentStatus.Pending,
                    IsApproved = false,
                    IsDeleted = false,
                    HomeServiceId = 3,
                    CustomerId = 3
                },
                new Orders
                {
                    Id = 4,
                    RequestDate = DateTime.Now.AddDays(-5),
                    ExecutionDate = DateTime.Now.AddDays(3),
                    ExecutionTime = TimeSpan.FromDays(3),
                    Description = "پروژه طراحی و توسعه فروشگاه آنلاین",
                    BasePrice = 3000.00m,
                    OrderStatus = OrderStatus.Completed, 
                    PaymentStatus = PaymentStatus.Pending,
                    IsApproved = true,
                    IsDeleted = false,
                    HomeServiceId = 4,
                    CustomerId = 2
                },
                new Orders
                {
                    Id = 5,
                    RequestDate = DateTime.Now.AddDays(-10),
                    ExecutionDate = DateTime.Now.AddDays(20),
                    ExecutionTime = TimeSpan.FromDays(15),
                    Description = "پروژه مشاوره و آموزش آنلاین",
                    BasePrice = 1000.00m,
                    OrderStatus = OrderStatus.WaitingForExpertSelection,  
                    PaymentStatus = PaymentStatus.Paid,
                    IsApproved = true,
                    IsDeleted = false,
                    HomeServiceId = 5,
                    CustomerId = 3
                }
            );
        }
    }
}
