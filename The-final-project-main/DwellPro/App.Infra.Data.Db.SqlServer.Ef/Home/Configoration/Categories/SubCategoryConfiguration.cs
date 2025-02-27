using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using App.Domain.Core.Home.Entities.Categories;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Categories
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");

            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(sc => sc.ImageUrl)
                   .HasMaxLength(500);

            builder.HasMany(sc => sc.HomeServices)
                   .WithOne(hs => hs.SubCategory)
                   .HasForeignKey(hs => hs.SubCategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(sc => sc.IsDeleted).HasDefaultValue(false);

            builder.HasData(
                new SubCategory { Id = 1, Name = "نظافت و پذیرایی", ImageUrl = "/images/sub/01-tamizkary/01.webp", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "شستشوی ", ImageUrl = "/images/sub/01-tamizkary/02.webp", CategoryId = 1 },
                new SubCategory { Id = 3, Name = " کارواش", ImageUrl = "/images/sub/01-tamizkary/03.webp", CategoryId = 1 },


                new SubCategory { Id = 4, Name = "سرمایشی و گرمایشی", ImageUrl = "/images/sub/02-sakhteman/08.webp", CategoryId = 2 },
                new SubCategory { Id = 5, Name = "تعمیرات ساختمان", ImageUrl = "/images/sub/02-sakhteman/07.webp", CategoryId = 2 },
                new SubCategory { Id = 6, Name = "لوله کشی", ImageUrl = "/images/sub/02-sakhteman/04.webp", CategoryId = 2 },
                new SubCategory { Id = 7, Name = "طراحی و بازسازی ساختمان", ImageUrl = "/images/sub/02-sakhteman/06.webp", CategoryId = 2 },
                new SubCategory { Id = 8, Name = "برق‌کاری", ImageUrl = "/images/sub/02-sakhteman/05.webp", CategoryId = 2 },

                new SubCategory { Id = 9, Name = "سرمایشی و گرمایشی", ImageUrl = "/images/sub/03-tamiratAshya/10.webp", CategoryId = 3 },
                new SubCategory { Id = 10, Name = "نصب و تعمیر لوازم خانگی", ImageUrl = "/images/sub/03-tamiratAshya/09.webp", CategoryId = 3 },
                new SubCategory { Id = 11, Name = "خدمات کامپیوتری", ImageUrl = "/images/sub/03-tamiratAshya/12.webp", CategoryId = 3 },
                new SubCategory { Id = 12, Name = "تعمیرات موبایل", ImageUrl = "/images/sub/03-tamiratAshya/11.webp", CategoryId = 3 },

                new SubCategory { Id = 13, Name = "باربری و جابه‌جایی", ImageUrl = "/images/sub/04-hamlBar/13.webp", CategoryId = 4 },

                new SubCategory { Id = 14, Name = "خدمات و تعمیر خودرو", ImageUrl = "/images/sub/05-khodro/14.webp", CategoryId = 5 },
                new SubCategory { Id = 15, Name = "کارواش", ImageUrl = "/images/sub/05-khodro/15.webp", CategoryId = 5 },

                new SubCategory { Id = 16, Name = "زیبایی بانوان", ImageUrl = "/images/sub/06-salamat-zibayi/17.webp", CategoryId = 6 },
                new SubCategory { Id = 17, Name = "پزشکی و پرستاری", ImageUrl = "/images/sub/06-salamat-zibayi/18.webp", CategoryId = 6 },
                new SubCategory { Id = 18, Name = "حیوانات خانگی", ImageUrl = "/images/sub/06-salamat-zibayi/19.webp", CategoryId = 6 },
                new SubCategory { Id = 19, Name = "مشاوره", ImageUrl = "/images/sub/06-salamat-zibayi/16.webp", CategoryId = 6 },
                new SubCategory { Id = 20, Name = "پیرایش و زیبایی آقایان", ImageUrl = "/images/sub/06-salamat-zibayi/21.webp", CategoryId = 6 },
                new SubCategory { Id = 21, Name = "ورزش", ImageUrl = "/images/sub/06-salamat-zibayi/20.webp", CategoryId = 6 },

                new SubCategory { Id = 22, Name = "خدمات شرکتی", ImageUrl = "/images/sub/07-sazman/22.webp", CategoryId = 7 },
                new SubCategory { Id = 23, Name = "تامین نیروی انسانی", ImageUrl = "/images/sub/07-sazman/t.webp", CategoryId = 7 },

                new SubCategory { Id = 24, Name = "خیاطی و تعمیرات لباس", ImageUrl = "/images/sub/08-sayer/25.webp", CategoryId = 8 },
                new SubCategory { Id = 25, Name = "مجالس و رویدادها", ImageUrl = "/images/sub/08-sayer/23.webp", CategoryId = 8 },
                new SubCategory { Id = 26, Name = "آموزش", ImageUrl = "/images/sub/08-sayer/27.webp", CategoryId = 8 },
                new SubCategory { Id = 27, Name = "خدمات فوری آچاره", ImageUrl = "/images/sub/08-sayer/24.webp", CategoryId = 8 },
                new SubCategory { Id = 28, Name = "همه فن حریف", ImageUrl = "/images/sub/08-sayer/26.webp", CategoryId = 8 }
            );
        }
    }
}
