using App.Domain.Core.Home.Entities.Categories;
using App.Domain.Core.Home.Entities.ListOrder;
using App.Domain.Core.Home.Entities.Other;
using App.Domain.Core.Home.Entities.Users;
using App.Infra.Data.Db.SqlServer.Ef.Home.Configoration;
using App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Categories;
using App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.ListOrder;
using App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Other;
using App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<ExpertProposal> ExpertProposals { get; set; }
        public DbSet<Experts> Experts { get; set; }
        public DbSet<ExpertHomeService> ExpertSkills { get; set; }
        public DbSet<HomeService> HomeServices { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Pictures> Pictures { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole<int>>().HasData(
           new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
           new IdentityRole<int> { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" },
           new IdentityRole<int> { Id = 3, Name = "Expert", NormalizedName = "EXPERT" }
       );

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 6, RoleId = 2 }
            );
            modelBuilder.Entity<Customers>().ToTable("Customers");
            modelBuilder.Entity<Experts>().ToTable("Experts");
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new ExpertProposalConfiguration());
            modelBuilder.ApplyConfiguration(new ExpertHomeServiceConfiguration());

            modelBuilder.ApplyConfiguration(new PicturesConfiguration());
            modelBuilder.ApplyConfiguration(new HomeServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ExpertsConfiguration());
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
            modelBuilder.ApplyConfiguration(new CommentsConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
        }
    }
}
