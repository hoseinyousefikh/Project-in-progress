using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using App.Domain.Core.Home.Entities.Categories;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.ImageUrl)
                   .HasMaxLength(500);

            builder.HasMany(c => c.SubCategories)
                   .WithOne(sc => sc.Category)
                   .HasForeignKey(sc => sc.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

            builder.HasData(
                new Category { Id = 1, Name = "تمیز کاری", ImageUrl = "/images/icon/01.webp" },
                new Category { Id = 2, Name = "ساختمان", ImageUrl = "/images/icon/08.webp" },
                new Category { Id = 3, Name = "تعمیرات اشیا", ImageUrl = "/images/icon/03.webp" },
                new Category { Id = 4, Name = "اسباب‌کشی و حمل بار", ImageUrl = "/images/icon/05.webp" },
                new Category { Id = 5, Name = "خودرو", ImageUrl = "/images/icon/06.webp" },
                new Category { Id = 6, Name = "سلامت و زیبایی", ImageUrl = "/images/icon/04.webp" },
                new Category { Id = 7, Name = "سازمان‌ها و مجتمع‌ها", ImageUrl = "/images/icon/02.webp" },
                new Category { Id = 8, Name = "سایر", ImageUrl = "/images/icon/07.webp" }
            );
        }
    }
}
