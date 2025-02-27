using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Core.Home.Entities.Categories;
using System.Collections.Generic;

namespace App.Infra.Data.Db.SqlServer.Ef.Home.Configoration.Categories
{
    public class HomeServiceConfiguration : IEntityTypeConfiguration<HomeService>
    {
        public void Configure(EntityTypeBuilder<HomeService> builder)
        {
            builder.ToTable("HomeServices");

            builder.HasKey(hs => hs.Id);

            builder.Property(hs => hs.Name).IsRequired().HasMaxLength(100);
            builder.Property(hs => hs.Description).IsRequired().HasMaxLength(500);
            builder.Property(hs => hs.BasePrice).IsRequired();
            builder.Property(hs => hs.ViewCount).HasDefaultValue(0);
            builder.Property(hs => hs.ImageUrl).HasMaxLength(500);

            builder.HasOne(hs => hs.SubCategory)
                   .WithMany(sc => sc.HomeServices)
                   .HasForeignKey(hs => hs.SubCategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(hs => hs.IsDeleted).HasDefaultValue(false);

            builder.HasData(
                new HomeService
                {
                    Id = 1,
                    Name = "سرویس عادی نظافت",
                    ImageUrl = "/images/HomeService/01-nezafat/08.jpg",
                    Description = "نظافت معمولی برای منازل و دفاتر شامل گردگیری، جاروکشی و تی کشی",
                    BasePrice = 500000,
                    SubCategoryId = 1
                },
                new HomeService
                {
                    Id = 2,
                    Name = "سرویس ویژه نظافت",
                    ImageUrl = "/images/HomeService/01-nezafat/03.jpg",
                    Description = "نظافت ویژه شامل شستشوی کامل کف، دیوارها و وسایل خانه",
                    BasePrice = 800000,
                    SubCategoryId = 1
                },
                new HomeService
                {
                    Id = 3,
                    Name = "سرویس لوکس نظافت",
                    ImageUrl = "/images/HomeService/01-nezafat/01.jpg",
                    Description = "نظافت تخصصی همراه با استفاده از مواد شوینده خاص و خوشبوکننده‌های ویژه",
                    BasePrice = 1200000,
                    SubCategoryId = 1
                },
                new HomeService
                {
                    Id = 4,
                    Name = "نظافت راه پله",
                    ImageUrl = "/images/HomeService/01-nezafat/02.jpg",
                    Description = "تمیز کردن و شستشوی کامل راه پله‌ها و پاگردها",
                    BasePrice = 600000,
                    SubCategoryId = 1
                },
                new HomeService
                {
                    Id = 5,
                    Name = "سرویس نظافت فوری",
                    ImageUrl = "/images/HomeService/01-nezafat/07.jpg",
                    Description = "نظافت اضطراری با تیم حرفه‌ای در کمترین زمان ممکن",
                    BasePrice = 700000,
                    SubCategoryId = 1
                },
                new HomeService
                {
                    Id = 6,
                    Name = "پذیرایی",
                    ImageUrl = "/images/HomeService/01-nezafat/04.jpg",
                    Description = "ارائه خدمات پذیرایی در مراسمات، مهمانی‌ها و رویدادهای خاص",
                    BasePrice = 900000,
                    SubCategoryId = 1
                },
                new HomeService
                {
                    Id = 7,
                    Name = "کارگر ساده",
                    ImageUrl = "/images/HomeService/01-nezafat/06.jpg",
                    Description = "اعزام کارگر ساده جهت کمک در امور نظافتی و خدماتی",
                    BasePrice = 550000,
                    SubCategoryId = 1
                },
                new HomeService
                {
                    Id = 8,
                    Name = "سمپاشی فضای داخلی",
                    ImageUrl = "/images/HomeService/01-nezafat/05.jpg",
                    Description = "سمپاشی منازل و ادارات برای از بین بردن حشرات موذی",
                    BasePrice = 950000,
                    SubCategoryId = 1
                },
                //****************************************************************************************
                new HomeService
                {
                    Id = 9,
                    Name = "شستشو در محل",
                    ImageUrl = "/images/HomeService/02-shostesho/04.jpg",
                    Description = "شستشوی انواع فرش، مبلمان و سطوح در محل با استفاده از تجهیزات پیشرفته.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 2
                },
                new HomeService
                {
                    Id = 10,
                    Name = "قالیشویی",
                    ImageUrl = "/images/HomeService/02-shostesho/03.jpg",
                    Description = "شستشوی انواع فرش و قالی با رعایت اصول استاندارد و مواد شوینده‌ی مناسب.",
                    BasePrice = 700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 2
                },
                new HomeService
                {
                    Id = 11,
                    Name = "خشکشویی",
                    ImageUrl = "/images/HomeService/02-shostesho/01.jpg",
                    Description = "خشکشویی لباس، پرده و ملحفه با دستگاه‌های پیشرفته و مواد شوینده‌ی استاندارد.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 2
                },
                new HomeService
                {
                    Id = 12,
                    Name = "پرده‌شویی",
                    ImageUrl = "/images/HomeService/02-shostesho/02.jpg",
                    Description = "شستشو و اتو کردن انواع پرده با حفظ کیفیت و جلوگیری از آسیب به پارچه.",
                    BasePrice = 450000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 2
                },
                //**************************************************************
                  new HomeService
                  {
                      Id = 13,
                      Name = "سرامیک خودرو",
                      ImageUrl = "/images/HomeService/03-karvash1/09.jpg",
                      Description = "سرامیک بدنه خودرو برای محافظت در برابر خط و خش و آلودگی",
                      BasePrice = 2500000,
                      SubCategoryId = 3
                  },
                new HomeService
                {
                    Id = 14,
                    Name = "کارواش نانو",
                    ImageUrl = "/images/HomeService/03-karvash1/02.jpg",
                    Description = "شستشوی خودرو با تکنولوژی نانو بدون نیاز به آب",
                    BasePrice = 600000,
                    SubCategoryId = 3
                },
                new HomeService
                {
                    Id = 15,
                    Name = "کارواش با آب",
                    ImageUrl = "/images/HomeService/03-karvash1/01.jpg",
                    Description = "شستشوی کامل بدنه خودرو با آب و مواد شوینده استاندارد",
                    BasePrice = 500000,
                    SubCategoryId = 3
                },
                new HomeService
                {
                    Id = 16,
                    Name = "واکس و پولیش خودرو",
                    ImageUrl = "/images/HomeService/03-karvash1/04.jpg",
                    Description = "واکس و پولیش حرفه‌ای برای افزایش درخشندگی و ماندگاری رنگ خودرو",
                    BasePrice = 800000,
                    SubCategoryId = 3
                },
                new HomeService
                {
                    Id = 17,
                    Name = "صفر شویی خودرو",
                    ImageUrl = "/images/HomeService/03-karvash1/05.jpg",
                    Description = "نظافت کامل داخل خودرو شامل صندلی‌ها، داشبورد و کفپوش",
                    BasePrice = 1200000,
                    SubCategoryId = 3
                },
                new HomeService
                {
                    Id = 18,
                    Name = "موتور شویی خودرو",
                    ImageUrl = "/images/HomeService/03-karvash1/06.jpg",
                    Description = "شستشوی موتور خودرو با مواد مخصوص بدون آسیب به اجزای الکتریکی",
                    BasePrice = 700000,
                    SubCategoryId = 3
                },
                new HomeService
                {
                    Id = 19,
                    Name = "احیای رنگ خودرو",
                    ImageUrl = "/images/HomeService/03-karvash1/03.jpg",
                    Description = "برطرف کردن خط و خش‌های سطحی و براق‌سازی مجدد رنگ خودرو",
                    BasePrice = 2000000,
                    SubCategoryId = 3
                },
                new HomeService
                {
                    Id = 20,
                    Name = "صافکاری و نقاشی خودرو",
                    ImageUrl = "/images/HomeService/03-karvash1/07.jpg",
                    Description = "صافکاری و نقاشی کامل خودرو بدون نیاز به باز و بست قطعات",
                    BasePrice = 5000000,
                    SubCategoryId = 3
                },
                new HomeService
                {
                    Id = 21,
                    Name = "نصب شیشه دودی",
                    ImageUrl = "/images/HomeService/03-karvash1/08.jpg",
                    Description = "نصب شیشه دودی استاندارد و مجاز برای خودرو",
                    BasePrice = 800000,
                    SubCategoryId = 3
                },
                //***********************************************************************
                new HomeService
                {
                    Id = 22,
                    Name = "تعمیر سرویس آبگرمکن",
                    ImageUrl = "/images/HomeService/04-sarmayesh/07.jpg",
                    Description = "تعمیر و سرویس انواع آبگرمکن‌های دیواری و ایستاده",
                    BasePrice = 1000000,
                    SubCategoryId = 4
                },
                new HomeService
                {
                    Id = 23,
                    Name = "نصب و تعمیر رادیاتور شوفاژ",
                    ImageUrl = "/images/HomeService/04-sarmayesh1/08.jpg",
                    Description = "نصب و تعمیر انواع شوفاژ و سیستم‌های گرمایشی",
                    BasePrice = 1200000,
                    SubCategoryId = 4
                },
                new HomeService
                {
                    Id = 24,
                    Name = "تعمیر و نگهداری موتورخانه",
                    ImageUrl = "/images/HomeService/04-sarmayesh/02.jpg",
                    Description = "سرویس و نگهداری سیستم‌های موتورخانه‌ای",
                    BasePrice = 2500000,
                    SubCategoryId = 4
                },
                new HomeService
                {
                    Id = 25,
                    Name = "نصب و تعمیر بخاری گازی",
                    ImageUrl = "/images/HomeService/04-sarmayesh/03.jpg",
                    Description = "نصب و تعمیر بخاری گازی و دودکش استاندارد",
                    BasePrice = 800000,
                    SubCategoryId = 4
                },
                new HomeService
                {
                    Id = 26,
                    Name = "تعمیر کولر گازی",
                    ImageUrl = "/images/HomeService/04-sarmayesh/01.jpg",
                    Description = "تعمیر و شارژ گاز انواع کولرهای گازی",
                    BasePrice = 1500000,
                    SubCategoryId = 4
                },
                new HomeService
                {
                    Id = 27,
                    Name = "تعمیر و سرویس فن",
                    ImageUrl = "/images/HomeService/04-sarmayesh/04.jpg",
                    Description = "سرویس و تعمیر انواع فن‌های تهویه مطبوع",
                    BasePrice = 500000,
                    SubCategoryId = 4
                },
                new HomeService
                {
                    Id = 28,
                    Name = "سرویس و تعمیر چیلر",
                    ImageUrl = "/images/HomeService/04-sarmayesh/06.jpg",
                    Description = "تعمیر و نگهداری چیلرهای صنعتی و خانگی",
                    BasePrice = 4000000,
                    SubCategoryId = 4
                },
                new HomeService
                {
                    Id = 29,
                    Name = "کانال‌سازی کولر",
                    ImageUrl = "/images/HomeService/04-sarmayesh/05.jpg",
                    Description = "ساخت و نصب کانال کولر با استانداردهای بهینه‌سازی",
                    BasePrice = 1800000,
                    SubCategoryId = 4
                },
                //********************************************************************************
                new HomeService
                {
                    Id = 30,
                    Name = "سنگ کاری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/18.jpg",
                    Description = "نصب و اجرای سنگ نما و کف",
                    BasePrice = 3000000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 31,
                    Name = "نصب کاغذ دیواری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/19.jpg",
                    Description = "نصب و اجرای انواع کاغذ دیواری مدرن",
                    BasePrice = 1500000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 32,
                    Name = "ساخت و نصب توری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/15.jpg",
                    Description = "ساخت و نصب توری پنجره و درب",
                    BasePrice = 800000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 33,
                    Name = "پتینه و دیوارنگاری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/02.jpg",
                    Description = "اجرای نقاشی دکوراتیو و پتینه‌کاری دیوارها",
                    BasePrice = 2500000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 34,
                    Name = "جوشکاری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/05.jpg",
                    Description = "اجرای جوشکاری برای درب و پنجره‌های فلزی",
                    BasePrice = 1800000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 35,
                    Name = "آهنگری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/06.jpg",
                    Description = "ساخت و تعمیر قطعات آهنی و سازه‌های فلزی",
                    BasePrice = 2000000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 36,
                    Name = "دوخت و نصب پرده",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/07.jpg",
                    Description = "طراحی، دوخت و نصب پرده‌های سفارشی",
                    BasePrice = 1000000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 37,
                    Name = "کاشی کاری و سرامیک‌کاری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/08.jpg",
                    Description = "نصب کاشی و سرامیک کف و دیوار",
                    BasePrice = 3000000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 38,
                    Name = "نصب کفپوش",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/10.jpg",
                    Description = "اجرای کفپوش پارکت و لمینت",
                    BasePrice = 2000000,
                    SubCategoryId = 5
                },
                new HomeService
                {
                    Id = 39,
                    Name = "گچ کاری و گچ‌بری",
                    ImageUrl = "/images/HomeService/05-tamirSakhteman/17.jpg",
                    Description = "اجرای گچ‌بری سقف و دیوار",
                    BasePrice = 2500000,
                    SubCategoryId = 5
                },
                //**********************************************************************
                new HomeService
                {
                    Id = 40,
                    Name = "تخلیه چاه و لوله بازکنی",
                    ImageUrl = "/images/HomeService/06-lolekeshi/08.jpg",
                    Description = "خدمات تخصصی تخلیه چاه و لوله بازکنی با تجهیزات پیشرفته و بدون تخریب.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 41,
                    Name = "تعمیر شیرآلات",
                    ImageUrl = "/images/HomeService/06-lolekeshi/10.jpg",
                    Description = "نصب و تعمیر انواع شیرآلات ساختمانی و صنعتی با بهترین کیفیت.",
                    BasePrice = 350000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 42,
                    Name = "پمپ و منبع آب",
                    ImageUrl = "/images/HomeService/06-lolekeshi/01.jpg",
                    Description = "نصب، تعمیر و سرویس انواع پمپ آب و منبع ذخیره آب.",
                    BasePrice = 600000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 43,
                    Name = "نصب و تعمیر دستگاه تصفیه",
                    ImageUrl = "/images/HomeService/06-lolekeshi/02.jpg",
                    Description = "نصب و تعمیر دستگاه‌های تصفیه آب خانگی و صنعتی با ضمانت کیفیت.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 44,
                    Name = "تعمیر و تشخیص ترکیدگی لوله",
                    ImageUrl = "/images/HomeService/06-lolekeshi/03.jpg",
                    Description = "تشخیص ترکیدگی لوله بدون تخریب و تعمیر با استفاده از دستگاه‌های پیشرفته.",
                    BasePrice = 750000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 45,
                    Name = "نصب سرویس و توالت فرنگی",
                    ImageUrl = "/images/HomeService/06-lolekeshi/04.jpg",
                    Description = "نصب و تعمیر انواع سرویس بهداشتی و توالت فرنگی با بهترین تجهیزات.",
                    BasePrice = 450000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 46,
                    Name = "نصب و تعمیر فلاش تانک",
                    ImageUrl = "/images/HomeService/06-lolekeshi/05.jpg",
                    Description = "نصب، تعمیر و تعویض انواع فلاش تانک توکار و روکار.",
                    BasePrice = 400000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 47,
                    Name = "لوله کشی گاز",
                    ImageUrl = "/images/HomeService/06-lolekeshi/06.jpg",
                    Description = "لوله‌کشی گاز ساختمان با رعایت استانداردهای ایمنی و استفاده از تجهیزات مناسب.",
                    BasePrice = 900000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 48,
                    Name = "نصب و تعمیر سینک ظرفشویی",
                    ImageUrl = "/images/HomeService/06-lolekeshi/07.jpg",
                    Description = "نصب و تعمیر انواع سینک ظرفشویی و رفع نشتی آب.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                new HomeService
                {
                    Id = 49,
                    Name = "لوله‌کشی آب و فاضلاب",
                    ImageUrl = "/images/HomeService/06-lolekeshi/08.jpg",
                    Description = "اجرای لوله‌کشی آب و فاضلاب برای ساختمان‌های مسکونی و تجاری.",
                    BasePrice = 850000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 6
                },
                //*******************************************************************************
                  new HomeService
                  {
                      Id = 50,
                      Name = "طراحی داخلی منزل",
                      ImageUrl = "/images/HomeService/07-tarahiSakhteman/01.webp",
                      Description = "طراحی داخلی منزل با جدیدترین متدهای روز و استفاده از متریال باکیفیت.",
                      BasePrice = 2500000,
                      ViewCount = 0,
                      IsDeleted = false,
                      SubCategoryId = 7
                  },
                new HomeService
                {
                    Id = 51,
                    Name = "طراحی دکوراسیون اداری",
                    ImageUrl = "/images/HomeService/07-tarahiSakhteman/08.jpg",
                    Description = "طراحی دکوراسیون فضای اداری و تجاری برای ایجاد محیطی شیک و کاربردی.",
                    BasePrice = 3500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 52,
                    Name = "بازسازی و نوسازی ساختمان",
                    ImageUrl = "/images/home_services/renovation.jpg",
                    Description = "بازسازی کامل ساختمان شامل طراحی داخلی، دیوارکشی، کف‌سازی و نورپردازی.",
                    BasePrice = 5000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 53,
                    Name = "نصب پارکت و لمینت",
                    ImageUrl = "/images/home_services/parquet_installation.jpg",
                    Description = "نصب انواع پارکت و لمینت با طرح‌ها و رنگ‌های متنوع.",
                    BasePrice = 1200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 54,
                    Name = "کاغذ دیواری و پوستر دیواری",
                    ImageUrl = "/images/home_services/wallpaper_installation.jpg",
                    Description = "نصب کاغذ دیواری و پوسترهای دیواری با طرح‌های مدرن و کلاسیک.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 55,
                    Name = "طراحی و اجرای سقف کاذب",
                    ImageUrl = "/images/home_services/false_ceiling.jpg",
                    Description = "اجرای سقف کاذب و نورپردازی سقف برای جلوه‌ای خاص در دکوراسیون داخلی.",
                    BasePrice = 1800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 56,
                    Name = "طراحی و ساخت کابینت آشپزخانه",
                    ImageUrl = "/images/home_services/kitchen_cabinet.jpg",
                    Description = "طراحی، ساخت و نصب کابینت‌های مدرن و کلاسیک برای آشپزخانه.",
                    BasePrice = 3000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 57,
                    Name = "نصب دیوارپوش و کفپوش PVC",
                    ImageUrl = "/images/home_services/pvc_floor_wall.jpg",
                    Description = "نصب دیوارپوش و کفپوش PVC مناسب برای فضاهای مسکونی و تجاری.",
                    BasePrice = 1300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 58,
                    Name = "نورپردازی داخلی و خارجی",
                    ImageUrl = "/images/home_services/lighting_design.jpg",
                    Description = "اجرای انواع نورپردازی داخلی و خارجی با طراحی مدرن و حرفه‌ای.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                new HomeService
                {
                    Id = 59,
                    Name = "نقاشی ساختمان",
                    ImageUrl = "/images/home_services/painting_home.jpg",
                    Description = "نقاشی ساختمان با رنگ‌های باکیفیت و اجرای طرح‌های متنوع.",
                    BasePrice = 900000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 7
                },
                //*********************************************************************************
                new HomeService
                {
                    Id = 60,
                    Name = "سیم کشی و کابل کشی",
                    ImageUrl = "/images/HomeService/08-Baghkary/10.jpg",
                    Description = "اجرای سیم‌کشی و کابل‌کشی استاندارد برای ساختمان‌های مسکونی و اداری.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 61,
                    Name = "نصب و تعمیر آیفون صوتی و تصویری",
                    ImageUrl = "/images/HomeService/08-Baghkary/03.jpg",
                    Description = "نصب و تعمیر انواع آیفون‌های صوتی و تصویری با تجهیزات مدرن.",
                    BasePrice = 1200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 62,
                    Name = "نصب لوستر و چراغ",
                    ImageUrl = "/images/HomeService/08-Baghkary/04.jpg",
                    Description = "نصب انواع لوستر و چراغ‌های سقفی و دیواری با رعایت ایمنی.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 63,
                    Name = "رفع خرابی و سیم کشی مجدد",
                    ImageUrl = "/images/HomeService/08-Baghkary/01.jpg",
                    Description = "بررسی و رفع خرابی‌های سیم‌کشی برق و ارتقا سیستم برق ساختمان.",
                    BasePrice = 1800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 64,
                    Name = "نصب و تعمیر تلویزیون",
                    ImageUrl = "/images/HomeService/08-Baghkary/05.jpg",
                    Description = "نصب، تنظیم و تعمیر انواع تلویزیون‌های LCD و LED.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 65,
                    Name = "نصب و تعمیر دوربین مداربسته",
                    ImageUrl = "/images/HomeService/08-Baghkary/06.jpg",
                    Description = "نصب و راه‌اندازی دوربین‌های مداربسته و سیستم‌های نظارتی.",
                    BasePrice = 2500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 66,
                    Name = "نصب و تعمیر کلید و پریز",
                    ImageUrl = "/images/HomeService/08-Baghkary/07.jpg",
                    Description = "نصب، تعویض و تعمیر انواع کلید و پریز برق با رعایت استانداردهای ایمنی.",
                    BasePrice = 700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 67,
                    Name = "نصب و تعویض فیوز",
                    ImageUrl = "/images/HomeService/08-Baghkary/11.jpg",
                    Description = "نصب، تعویض و ارتقا فیوزهای برق ساختمان برای افزایش ایمنی.",
                    BasePrice = 900000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 68,
                    Name = "تعمیر و سرویس آسانسور",
                    ImageUrl = "/images/HomeService/08-Baghkary/08.jpg",
                    Description = "تعمیر، نگهداری و سرویس دوره‌ای آسانسورهای مسکونی و تجاری.",
                    BasePrice = 3000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 69,
                    Name = "نصب و تعمیر کرکره برقی",
                    ImageUrl = "/images/HomeService/08-Baghkary/09.jpg",
                    Description = "نصب و تعمیر کرکره‌های برقی برای مغازه‌ها و پارکینگ‌ها.",
                    BasePrice = 2800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                new HomeService
                {
                    Id = 70,
                    Name = "نصب هواکش و تهویه مطبوع",
                    ImageUrl = "/images/HomeService/08-Baghkary/02.jpg",
                    Description = "نصب انواع هواکش و تهویه مطبوع برای ساختمان‌های مسکونی و تجاری.",
                    BasePrice = 1700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 8
                },
                //*************************************************************************
                 new HomeService
                 {
                     Id = 71,
                     Name = "تعمیر کولرگازی",
                     ImageUrl = "/images/HomeService/09-sarmayesh/01.jpg",
                     Description = "تعمیر و سرویس کولرهای گازی و سیستم‌های تهویه مطبوع.",
                     BasePrice = 2000000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 9
                 },
                new HomeService
                {
                    Id = 72,
                    Name = "تعمیر و سرویس فن",
                    ImageUrl = "/images/HomeService/09-sarmayesh/04.jpg",
                    Description = "تعمیر و سرویس انواع فن‌ها برای تهویه هوا در ساختمان‌ها.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 9
                },
                new HomeService
                {
                    Id = 73,
                    Name = "سرویس و تعمیر چیلر",
                    ImageUrl = "/images/HomeService/09-sarmayesh/06.jpg",
                    Description = "سرویس، تعمیر و نگهداری سیستم‌های چیلر برای تهویه مطبوع ساختمان‌ها.",
                    BasePrice = 4000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 9
                },
                new HomeService
                {
                    Id = 74,
                    Name = "کانال سازی کولر",
                    ImageUrl = "/images/HomeService/09-sarmayesh/05.jpg",
                    Description = "نصب و کانال‌سازی کولرهای گازی و مرکزی برای تهویه مناسب در فضاهای مختلف.",
                    BasePrice = 1800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 9
                },
                //****************************************************************************
                  new HomeService
                  {
                      Id = 75,
                      Name = "نصب و تعمیر یخچال فریزر",
                      ImageUrl = "/images/HomeService/10-nasbVtamir/09.jpg",
                      Description = "نصب و تعمیر انواع یخچال فریزرها با رعایت استانداردهای ایمنی.",
                      BasePrice = 2500000,
                      ViewCount = 0,
                      IsDeleted = false,
                      SubCategoryId = 10
                  },
                new HomeService
                {
                    Id = 76,
                    Name = "نصب و تعمیر لباسشویی",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/03.jpg",
                    Description = "نصب و تعمیر انواع ماشین‌های لباسشویی اتوماتیک و نیمه‌اتوماتیک.",
                    BasePrice = 2200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                new HomeService
                {
                    Id = 77,
                    Name = "نصب و تعمیر اجاق گاز",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/06.jpg",
                    Description = "نصب، تعمیر و سرویس اجاق گازهای رومیزی و فر دار.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                new HomeService
                {
                    Id = 78,
                    Name = "نصب و تعمیر ماشین ظرفشویی",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/02.jpg",
                    Description = "نصب و تعمیر انواع ماشین‌های ظرفشویی اتوماتیک و رومیزی.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                new HomeService
                {
                    Id = 79,
                    Name = "تعمیرات تخصصی تلویزیون",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/01.jpg",
                    Description = "تعمیرات تخصصی انواع تلویزیون‌های LED، LCD و OLED.",
                    BasePrice = 1800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                new HomeService
                {
                    Id = 80,
                    Name = "نصب و تعمیر ماکروویو",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/05.jpg",
                    Description = "نصب، تعمیر و سرویس ماکروویو و مایکروفرهای مختلف.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                new HomeService
                {
                    Id = 81,
                    Name = "نصب و تعمیر هود آشپزخانه",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/04.jpg",
                    Description = "نصب و تعمیر هودهای آشپزخانه با انواع سیستم‌های تهویه.",
                    BasePrice = 1200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                new HomeService
                {
                    Id = 82,
                    Name = "تعمیر جاروبرقی",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/08.jpg",
                    Description = "تعمیر و سرویس انواع جاروبرقی‌های خانگی و صنعتی.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                new HomeService
                {
                    Id = 83,
                    Name = "تعمیر چرخ خیاطی",
                    ImageUrl = "/images/HomeService/10-nasbVtamir/07.jpg",
                    Description = "تعمیر و سرویس انواع چرخ‌های خیاطی خانگی و صنعتی.",
                    BasePrice = 700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 10
                },
                //***********************************************************************
                  new HomeService
                  {
                      Id = 84,
                      Name = "تعمیر کامپیوتر و لپ‌تاپ",
                      ImageUrl = "/images/HomeService/11-khadamatcamputer/02.webp",
                      Description = "تعمیرات تخصصی کامپیوتر و لپ‌تاپ شامل سخت‌افزار و نرم‌افزار.",
                      BasePrice = 1500000,
                      ViewCount = 0,
                      IsDeleted = false,
                      SubCategoryId = 11
                  },
                new HomeService
                {
                    Id = 85,
                    Name = "تعمیر ماشین‌های اداری",
                    ImageUrl = "/images/HomeService/11-khadamatcamputer/04.webp",
                    Description = "تعمیرات تخصصی ماشین‌های اداری مانند پرینتر، فکس، کپی.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 11
                },
                new HomeService
                {
                    Id = 86,
                    Name = "پشتیبانی شبکه و سرور",
                    ImageUrl = "/images/HomeService/11-khadamatcamputer/01.webp",
                    Description = "راه‌اندازی و پشتیبانی شبکه‌های کامپیوتری و سرورهای سازمانی.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 11
                },
                new HomeService
                {
                    Id = 87,
                    Name = "مودم و اینترنت",
                    ImageUrl = "/images/HomeService/11-khadamatcamputer/03.webp",
                    Description = "نصب، تنظیم و تعمیر مودم‌ها و مشکلات مربوط به اینترنت.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 11
                },
                new HomeService
                {
                    Id = 88,
                    Name = "طراحی سایت و لوگو",
                    ImageUrl = "/images/HomeService/11-khadamatcamputer/05.jpg",
                    Description = "طراحی سایت‌های حرفه‌ای و لوگوهای برند برای کسب‌وکارها.",
                    BasePrice = 3000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 11
                },
                //*********************************************************************
                 new HomeService
                 {
                     Id = 89,
                     Name = "خدمات تعمیرات تاچ و ال‌سی‌دی",
                     ImageUrl = "/images/HomeService/12-tamiratMobile/02.jpg",
                     Description = "تعمیرات تخصصی صفحه نمایش‌های تاچ و LCD انواع دستگاه‌ها.",
                     BasePrice = 1000000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 12
                 },
                new HomeService
                {
                    Id = 90,
                    Name = "خدمات باتری",
                    ImageUrl = "/images/HomeService/12-tamiratMobile/08.jpg",
                    Description = "تعویض، تعمیر و سرویس انواع باتری گوشی، لپ‌تاپ و دستگاه‌های دیگر.",
                    BasePrice = 700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 12
                },
                new HomeService
                {
                    Id = 91,
                    Name = "خدمات عیب‌یابی و تعمیر برد",
                    ImageUrl = "/images/HomeService/12-tamiratMobile/04.jpg",
                    Description = "عیب‌یابی و تعمیر بردهای الکترونیکی دستگاه‌ها و تجهیزات مختلف.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 12
                },
                new HomeService
                {
                    Id = 92,
                    Name = "خدمات نرم‌افزاری",
                    ImageUrl = "/images/HomeService/12-tamiratMobile/05.jpg",
                    Description = "خدمات نرم‌افزاری شامل نصب، تعمیر و پشتیبانی نرم‌افزارها.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 12
                },
                new HomeService
                {
                    Id = 93,
                    Name = "خدمات اسپیکر",
                    ImageUrl = "/images/HomeService/12-tamiratMobile/06.jpg",
                    Description = "تعمیر و سرویس اسپیکرهای خانگی، بلوتوثی و دیگر انواع دستگاه‌های صوتی.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 12
                },
                new HomeService
                {
                    Id = 94,
                    Name = "خدمات فریم و قاب",
                    ImageUrl = "/images/HomeService/12-tamiratMobile/07.jpg",
                    Description = "تعویض و تعمیر فریم و قاب انواع دستگاه‌ها از جمله گوشی‌های موبایل.",
                    BasePrice = 600000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 12
                },
                new HomeService
                {
                    Id = 95,
                    Name = "خدمات دوربین",
                    ImageUrl = "/images/HomeService/12-tamiratMobile/09.jpg",
                    Description = "تعمیر، نصب و سرویس انواع دوربین‌های مدار بسته و دوربین‌های گوشی.",
                    BasePrice = 1200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 12
                },
                new HomeService
                {
                    Id = 96,
                    Name = "خدمات سنسور",
                    ImageUrl = "/images/HomeService/12-tamiratMobile/01.jpg",
                    Description = "تعمیر و تعویض انواع سنسورهای مختلف دستگاه‌ها و تجهیزات.",
                    BasePrice = 700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 12
                },
                //***************************************************************************
                  new HomeService
                  {
                      Id = 97,
                      Name = "اسباب کشی با وانت و نیسان",
                      ImageUrl = "/images/HomeService/13-BarbarivJabejayi/06.jpg",
                      Description = "خدمات اسباب کشی با استفاده از وانت و نیسان برای جابجایی راحت و سریع.",
                      BasePrice = 3000000,
                      ViewCount = 0,
                      IsDeleted = false,
                      SubCategoryId = 13
                  },
                new HomeService
                {
                    Id = 98,
                    Name = "حمل بار بین شهری",
                    ImageUrl = "/images/HomeService/13-BarbarivJabejayi/04.jpg",
                    Description = "حمل بار از شهری به شهر دیگر با تضمین ایمنی و سرعت بالا.",
                    BasePrice = 5000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 13
                },
                new HomeService
                {
                    Id = 99,
                    Name = "سرویس بسته بندی",
                    ImageUrl = "/images/HomeService/13-BarbarivJabejayi/07.jpg",
                    Description = "ارائه خدمات بسته بندی حرفه‌ای برای اسباب کشی و جابجایی کالا.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 13
                },
                new HomeService
                {
                    Id = 100,
                    Name = "کارگر جابه جایی",
                    ImageUrl = "/images/HomeService/13-BarbarivJabejayi/08.jpg",
                    Description = "اعزام کارگر برای کمک به جابه‌جایی و حمل وسایل.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 13
                },
                new HomeService
                {
                    Id = 101,
                    Name = "جابه جایی گاوصندوق",
                    ImageUrl = "/images/HomeService/13-BarbarivJabejayi/01.jpg",
                    Description = "خدمات جابه‌جایی گاوصندوق‌های سنگین با تجهیزات ویژه.",
                    BasePrice = 2500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 13
                },
                new HomeService
                {
                    Id = 102,
                    Name = "حمل نخاله و ضایعات ساختمانی",
                    ImageUrl = "/images/HomeService/13-BarbarivJabejayi/05.jpg",
                    Description = "حمل و نقل نخاله‌ها و ضایعات ساختمانی با ماشین‌های مخصوص.",
                    BasePrice = 3500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 13
                },
                new HomeService
                {
                    Id = 103,
                    Name = "اجاره انبار و سوله",
                    ImageUrl = "/images/HomeService/13-BarbarivJabejayi/02.jpg",
                    Description = "اجاره انبار و سوله برای نگهداری کالاها و وسایل.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 13
                },
                new HomeService
                {
                    Id = 104,
                    Name = "اسباب کشی شرکت‌ها",
                    ImageUrl = "/images/HomeService/13-BarbarivJabejayi/03.jpg",
                    Description = "خدمات اسباب‌کشی ویژه برای شرکت‌ها و ادارات.",
                    BasePrice = 4000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 13
                },
                //*****************************************************************
                 new HomeService
                 {
                     Id = 105,
                     Name = "باتری به باتری",
                     ImageUrl = "/images/HomeService/14-khadamatkhodro/01.jpg",
                     Description = "خدمات باتری به باتری خودرو در مواقع ضروری.",
                     BasePrice = 500000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 14
                 },
                new HomeService
                {
                    Id = 106,
                    Name = "برق و باتری خودرو",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/04.jpg",
                    Description = "تعمیرات و سرویس‌های برق خودرو و باتری‌های آن.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 107,
                    Name = "مکانیکی خودرو",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/05.jpg",
                    Description = "خدمات مکانیکی خودرو شامل تعمیرات و سرویس‌های مختلف.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 108,
                    Name = "امداد خودرو",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/10.jpg",
                    Description = "خدمات امداد جاده‌ای برای خودروهایی که دچار مشکل شده‌اند.",
                    BasePrice = 1200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 109,
                    Name = "کارشناسی خودرو",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/06.jpg",
                    Description = "خدمات کارشناسی خودرو برای خرید، فروش و ارزیابی وضعیت فنی خودرو.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 110,
                    Name = "حمل خودرو",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/02.jpg",
                    Description = "خدمات حمل خودرو از مکانی به مکان دیگر با تجهیزات تخصصی.",
                    BasePrice = 3000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 111,
                    Name = "پنچر گیری",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/03.jpg",
                    Description = "خدمات پنچرگیری برای انواع خودروها.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 112,
                    Name = "تعویض لاستیک",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/08.jpg",
                    Description = "خدمات تعویض لاستیک خودرو با توجه به نیاز و نوع خودرو.",
                    BasePrice = 700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 113,
                    Name = "تست دیاگ",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/09.jpg",
                    Description = "خدمات تست دیاگ خودرو برای شناسایی ایرادات و مشکلات فنی.",
                    BasePrice = 600000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 114,
                    Name = "تعویض لنت خودرو",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/11.jpg",
                    Description = "خدمات تعویض لنت خودرو برای بهبود عملکرد ترمز.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                },
                new HomeService
                {
                    Id = 115,
                    Name = "تعویض شمع خودرو",
                    ImageUrl = "/images/HomeService/14-khadamatkhodro/07.jpg",
                    Description = "خدمات تعویض شمع خودرو برای بهبود عملکرد موتور.",
                    BasePrice = 400000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 14
                }, 
                //**********************************************************************************
                new HomeService
                {
                    Id = 116,
                    Name = "سرامیک خودرو",
                    ImageUrl = "/images/HomeService/15-Karvash/08.jpg",
                    Description = "خدمات سرامیک بدنه خودرو برای حفظ رنگ و جلوگیری از آسیب‌ها.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                new HomeService
                {
                    Id = 117,
                    Name = "کارواش نانو",
                    ImageUrl = "/images/HomeService/15-Karvash/02.jpg",
                    Description = "کارواش با استفاده از تکنولوژی نانو برای شستشو و محافظت بیشتر.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                new HomeService
                {
                    Id = 118,
                    Name = "کارواش با آب",
                    ImageUrl = "/images/HomeService/15-Karvash/01.jpg",
                    Description = "خدمات کارواش با استفاده از آب برای شستشوی خودرو.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                new HomeService
                {
                    Id = 119,
                    Name = "واکس و پولیش خودرو",
                    ImageUrl = "/images/HomeService/15-Karvash/04.jpg",
                    Description = "خدمات واکس و پولیش برای براق کردن و حفظ بدنه خودرو.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                new HomeService
                {
                    Id = 120,
                    Name = "صفر شویی خودرو",
                    ImageUrl = "/images/HomeService/15-Karvash/09.jpg",
                    Description = "خدمات صفر شویی برای تمیز کردن کامل خودرو به صورت حرفه‌ای.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                new HomeService
                {
                    Id = 121,
                    Name = "موتور شویی خودرو",
                    ImageUrl = "/images/HomeService/15-Karvash/05.jpg",
                    Description = "خدمات شستشوی موتور خودرو برای حفظ عملکرد بهتر.",
                    BasePrice = 1200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                new HomeService
                {
                    Id = 122,
                    Name = "احیا رنگ خودرو",
                    ImageUrl = "/images/HomeService/15-Karvash/03.jpg",
                    Description = "خدمات احیای رنگ خودرو برای بازگرداندن درخشش و زیبایی رنگ.",
                    BasePrice = 2500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                new HomeService
                {
                    Id = 123,
                    Name = "نصب شیشه دودی",
                    ImageUrl = "/images/HomeService/15-Karvash/07.jpg",
                    Description = "نصب شیشه دودی برای جلوگیری از تابش نور مستقیم و حفظ حریم خصوصی.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 15
                },
                //******************************************************************
                 new HomeService
                 {
                     Id = 124,
                     Name = "خدمات ناخن",
                     ImageUrl = "/images/HomeService/16-zibayiBanovan/02.jpg",
                     Description = "خدمات آرایشی ناخن شامل کاشت و طراحی ناخن.",
                     BasePrice = 300000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 16
                 },
                new HomeService
                {
                    Id = 125,
                    Name = "رنگ مو بانوان",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/07.jpg",
                    Description = "خدمات رنگ مو برای بانوان با استفاده از محصولات باکیفیت.",
                    BasePrice = 600000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 126,
                    Name = "مش لایت بالیاژ",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/03.jpg",
                    Description = "خدمات مش لایت و بالیاژ برای ایجاد ظاهر مدرن و طبیعی.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 127,
                    Name = "براشینگ مو بانوان",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/05.jpg",
                    Description = "خدمات براشینگ مو برای ظاهر صاف و براق.",
                    BasePrice = 200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 128,
                    Name = "اصلاح صورت",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/04.jpg",
                    Description = "خدمات اصلاح صورت برای داشتن پوستی صاف و شاداب.",
                    BasePrice = 100000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 129,
                    Name = "کوتاهی مو بانوان",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/06.jpg",
                    Description = "خدمات کوتاهی مو بانوان با استفاده از تکنیک‌های روز.",
                    BasePrice = 400000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 130,
                    Name = "کراتینه و ویتامینه مو",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/10.jpg",
                    Description = "خدمات کراتینه و ویتامینه برای تقویت و نرم شدن مو.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 131,
                    Name = "کاشت مژه",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/01.jpg",
                    Description = "خدمات کاشت مژه برای داشتن مژه‌هایی بلند و جذاب.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 132,
                    Name = "لمینت و لیفت مژه",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/09.jpg",
                    Description = "خدمات لمینت و لیفت مژه برای فرم‌دهی به مژه‌ها.",
                    BasePrice = 600000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                new HomeService
                {
                    Id = 133,
                    Name = "اپیلاسیون در خانه",
                    ImageUrl = "/images/HomeService/16-zibayiBanovan/00.jpg",
                    Description = "خدمات اپیلاسیون در منزل برای راحتی شما.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 16
                },
                //**********************************************************************
                new HomeService
                {
                    Id = 134,
                    Name = "مراقبت و نگهداری",
                    ImageUrl = "/images/HomeService/17-Parastari/01.jpg",
                    Description = "خدمات مراقبت و نگهداری از بیمار در منزل.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 17
                },
                new HomeService
                {
                    Id = 135,
                    Name = "پرستاری و تزریقات",
                    ImageUrl = "/images/HomeService/17-Parastari/05.jpg",
                    Description = "خدمات پرستاری و تزریقات در منزل برای بیماران.",
                    BasePrice = 150000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 17
                },
                new HomeService
                {
                    Id = 136,
                    Name = "معاینه پزشکی",
                    ImageUrl = "/images/HomeService/17-Parastari/02.jpg",
                    Description = "خدمات معاینه پزشکی برای تشخیص بیماری‌ها و ارائه درمان‌های لازم.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 17
                },
                new HomeService
                {
                    Id = 137,
                    Name = "پیرا پزشکی",
                    ImageUrl = "/images/HomeService/17-Parastari/03.jpg",
                    Description = "خدمات پیراپزشکی شامل فیزیوتراپی، ارتوپدی، و سایر خدمات تخصصی.",
                    BasePrice = 400000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 17
                },
                new HomeService
                {
                    Id = 138,
                    Name = "آزمایش و نمونه گیری",
                    ImageUrl = "/images/HomeService/17-Parastari/04.jpg",
                    Description = "خدمات آزمایشگاهی و نمونه‌گیری در منزل برای تشخیص دقیق‌تر بیماری‌ها.",
                    BasePrice = 200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 17
                },
                //*****************************************************************************************
                 new HomeService
                 {
                     Id = 139,
                     Name = "هتل های حیوانات خانگی",
                     ImageUrl = "/images/HomeService/18-heyvanat/04.jpg",
                     Description = "خدمات هتل‌های حیوانات خانگی برای مراقبت از حیوانات در طول مدت غیبت صاحب آن‌ها.",
                     BasePrice = 1000000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 18
                 },
                new HomeService
                {
                    Id = 140,
                    Name = "خدمات دامپزشکی در محل",
                    ImageUrl = "/images/HomeService/18-heyvanat/02.jpg",
                    Description = "خدمات دامپزشکی در محل برای معاینه و درمان حیوانات خانگی.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 18
                },
                new HomeService
                {
                    Id = 141,
                    Name = "خدمات تربیتی حیوانات خانگی",
                    ImageUrl = "/images/HomeService/18-heyvanat/01.jpg",
                    Description = "خدمات تربیتی برای حیوانات خانگی جهت آموزش رفتارهای مطلوب.",
                    BasePrice = 700000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 18
                },
                new HomeService
                {
                    Id = 142,
                    Name = "خدمات شستشو حیوانات خانگی",
                    ImageUrl = "/images/HomeService/18-heyvanat/05.jpg",
                    Description = "خدمات شستشو و حمام برای حیوانات خانگی برای بهداشت و تمیزی.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 18
                },
                new HomeService
                {
                    Id = 143,
                    Name = "پت شاپ",
                    ImageUrl = "/images/HomeService/18-heyvanat/03.jpg",
                    Description = "فروشگاه لوازم حیوانات خانگی شامل غذا، اسباب‌بازی و لوازم جانبی.",
                    BasePrice = 100000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 18
                },
                //*********************************************************************************
                 new HomeService
                 {
                     Id = 144,
                     Name = "مشاوره مالی و مالیاتی",
                     ImageUrl = "/images/HomeService/19-Moshavere/01.jpg",
                     Description = "خدمات مشاوره در زمینه مسائل مالی و مالیاتی برای کسب‌وکارها و افراد.",
                     BasePrice = 500000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 19
                 },
                 //*****************************************************************
                  new HomeService
                  {
                      Id = 145,
                      Name = "کوتاهی موی سر و اصلاح صورت",
                      ImageUrl = "/images/HomeService/20-PirayeshAghayan/02.jpg",
                      Description = "خدمات کوتاهی موی سر و اصلاح صورت برای آقایان.",
                      BasePrice = 200000,
                      ViewCount = 0,
                      IsDeleted = false,
                      SubCategoryId = 20
                  },
                new HomeService
                {
                    Id = 146,
                    Name = "مراقبت های زیبایی آقایان",
                    ImageUrl = "/images/HomeService/20-PirayeshAghayan/03.jpg",
                    Description = "خدمات مراقبت از پوست، مو و زیبایی برای آقایان.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 20
                },
                new HomeService
                {
                    Id = 147,
                    Name = "گریم داماد",
                    ImageUrl = "/images/HomeService/20-PirayeshAghayan/01.jpg",
                    Description = "خدمات گریم و آرایش داماد برای روز عروسی.",
                    BasePrice = 500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 20
                },
                //************************************************************************
                 new HomeService
                 {
                     Id = 148,
                     Name = "کلاس سی ایکس در خانه",
                     ImageUrl = "/images/HomeService/21-Varzesh/03.jpg",
                     Description = "کلاس‌های تمرینی سی ایکس در منزل برای تقویت بدن و عضلات.",
                     BasePrice = 350000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 21
                 },
                new HomeService
                {
                    Id = 149,
                    Name = "برنامه ورزشی در خانه",
                    ImageUrl = "/images/HomeService/21-Varzesh/05.jpg",
                    Description = "ارائه برنامه‌های ورزشی اختصاصی در منزل برای تناسب اندام.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 21
                },
                new HomeService
                {
                    Id = 150,
                    Name = "کلاس یوگا در خانه",
                    ImageUrl = "/images/HomeService/21-Varzesh/02.jpg",
                    Description = "کلاس‌های یوگا در منزل برای افزایش انعطاف‌پذیری و آرامش ذهنی.",
                    BasePrice = 400000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 21
                },
                new HomeService
                {
                    Id = 151,
                    Name = "کلاس پیلاتس در خانه",
                    ImageUrl = "/images/HomeService/21-Varzesh/04.jpg",
                    Description = "کلاس‌های پیلاتس در منزل برای تقویت بدن و بهبود وضعیت posture.",
                    BasePrice = 350000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 21
                },
                new HomeService
                {
                    Id = 152,
                    Name = "کلاس فیتنس در خانه",
                    ImageUrl = "/images/HomeService/21-Varzesh/06.jpg",
                    Description = "کلاس‌های فیتنس برای افزایش قدرت بدنی و تناسب اندام در منزل.",
                    BasePrice = 400000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 21
                },
                new HomeService
                {
                    Id = 153,
                    Name = "حرکت اصلاحی",
                    ImageUrl = "/images/HomeService/21-Varzesh/01.jpg",
                    Description = "تمرینات اصلاحی برای بهبود وضعیت بدن و جلوگیری از آسیب‌ها.",
                    BasePrice = 250000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 21
                },
                //*************************************************************
                  new HomeService
                  {
                      Id = 154,
                      Name = "خدمات شرکتی با سازمان کوچک",
                      ImageUrl = "/images/HomeService/21-Varzesh/02.jpg",
                      Description = "خدمات مخصوص سازمان‌های کوچک مانند حسابداری، مشاوره، پشتیبانی و غیره.",
                      BasePrice = 1000000,
                      ViewCount = 0,
                      IsDeleted = false,
                      SubCategoryId = 22
                  },
                new HomeService
                {
                    Id = 155,
                    Name = "خدمات شرکتی با سازمان بزرگ",
                    ImageUrl = "/images/HomeService/21-Varzesh/03.jpg",
                    Description = "خدمات مدیریتی و پشتیبانی برای سازمان‌های بزرگ.",
                    BasePrice = 3000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 22
                },
                new HomeService
                {
                    Id = 156,
                    Name = "پشتیبانی فروش",
                    ImageUrl = "/images/HomeService/21-Varzesh/01.jpg",
                    Description = "خدمات پشتیبانی برای تیم‌های فروش از جمله آموزش، مشاوره، و ارائه راهکارهای فروش.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 22
                },
                //*********************************************************************************
                 new HomeService
                 {
                     Id = 157,
                     Name = "استخدام خدمتگزار",
                     ImageUrl = "/images/HomeService/23-TaminNiroyeEnsani/01.jpg",
                     Description = "خدمات مربوط به استخدام خدمتگزار خانگی برای انجام امور منزل.",
                     BasePrice = 2000000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 23
                 },
                 //*************************************************************************************************
                  new HomeService
                  {
                      Id = 158,
                      Name = "تعمیرات لباس",
                      ImageUrl = "/images/HomeService/24-TamiratLebas/02.jpg",
                      Description = "خدمات تعمیرات و ترمیم لباس‌های مختلف مانند پاره شدن و تغییر سایز.",
                      BasePrice = 100000,
                      ViewCount = 0,
                      IsDeleted = false,
                      SubCategoryId = 24
                  },
                new HomeService
                {
                    Id = 159,
                    Name = "دوخت لباس زنانه",
                    ImageUrl = "/images/HomeService/24-TamiratLebas/03.jpg",
                    Description = "خدمات دوخت و طراحی لباس‌های زنانه.",
                    BasePrice = 200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 24
                },
                new HomeService
                {
                    Id = 160,
                    Name = "تعمیر کیف و کفش",
                    ImageUrl = "/images/HomeService/24-TamiratLebas/01.jpg",
                    Description = "خدمات تعمیر کیف و کفش از جمله تعویض بند، ترمیم و رنگ‌آمیزی.",
                    BasePrice = 150000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 24
                },
                //***************************************************************
                 new HomeService
                 {
                     Id = 161,
                     Name = "کیک و شیرینی",
                     ImageUrl = "/images/HomeService/25-Majales/09.jpg",
                     Description = "خدمات تهیه کیک و شیرینی مخصوص مجالس و مهمانی‌ها.",
                     BasePrice = 1000000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 25
                 },
                new HomeService
                {
                    Id = 162,
                    Name = "ارسال هدیه",
                    ImageUrl = "/images/HomeService/25-Majales/01.jpg",
                    Description = "خدمات ارسال هدیه‌های متنوع برای عزیزان شما.",
                    BasePrice = 200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 163,
                    Name = "مهماندار مجالس",
                    ImageUrl = "/images/HomeService/25-Majales/02.jpg",
                    Description = "خدمات مهمانداری برای مجالس و مهمانی‌ها.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 164,
                    Name = "فینگر فود",
                    ImageUrl = "/images/HomeService/25-Majales/08.jpg",
                    Description = "خدمات آماده‌سازی و سرو فینگر فود برای مهمانی‌ها.",
                    BasePrice = 1500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 165,
                    Name = "عکاس و فیلمبرداری",
                    ImageUrl = "/images/HomeService/25-Majales/05.jpg",
                    Description = "خدمات عکاسی و فیلمبرداری حرفه‌ای برای مجالس و مهمانی‌ها.",
                    BasePrice = 5000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 166,
                    Name = "موزیک",
                    ImageUrl = "/images/HomeService/25-Majales/03.jpg",
                    Description = "خدمات موسیقی برای مجالس و جشن‌ها.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 167,
                    Name = "تشریفات مجالس",
                    ImageUrl = "/images/HomeService/25-Majales/07.jpg",
                    Description = "خدمات تشریفات و دکوراسیون برای مجالس و جشن‌ها.",
                    BasePrice = 4000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 168,
                    Name = "آشپری در محل",
                    ImageUrl = "/images/HomeService/25-Majales/06.jpg",
                    Description = "خدمات آشپزی و سرو غذا در محل برای مهمانی‌ها.",
                    BasePrice = 3500000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 169,
                    Name = "سفره آرایی",
                    ImageUrl = "/images/HomeService/25-Majales/10.jpg",
                    Description = "خدمات سفره‌آرایی برای مجالس و مهمانی‌ها.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                new HomeService
                {
                    Id = 170,
                    Name = "دکور تولد",
                    ImageUrl = "/images/HomeService/25-Majales/04.jpg",
                    Description = "خدمات دکوراسیون تولد و جشن‌های دیگر.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 25
                },
                //*************************************************************************
                 new HomeService
                 {
                     Id = 171,
                     Name = "آمادگی برای کنکور",
                     ImageUrl = "/images/HomeService/26-Amozesh/02.jpg",
                     Description = "خدمات آمادگی برای کنکور در رشته‌های مختلف.",
                     BasePrice = 1500000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 26
                 },
                new HomeService
                {
                    Id = 172,
                    Name = "دانشگاه گروه صنعتی گلرنگ",
                    ImageUrl = "/images/HomeService/26-Amozesh/01.jpg",
                    Description = "دوره‌های آموزشی دانشگاه گروه صنعتی گلرنگ.",
                    BasePrice = 5000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 26
                },
                new HomeService
                {
                    Id = 173,
                    Name = "آموزش زبان‌های خارجی",
                    ImageUrl = "/images/HomeService/26-Amozesh/04.jpg",
                    Description = "دوره‌های آموزش زبان‌های خارجی با اساتید مجرب.",
                    BasePrice = 1000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 26
                },
                new HomeService
                {
                    Id = 174,
                    Name = "آموزش ابتدایی تا متوسطه",
                    ImageUrl = "/images/HomeService/26-Amozesh/03.jpg",
                    Description = "دوره‌های آموزش دروس از ابتدایی تا متوسطه.",
                    BasePrice = 800000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 26
                },
                new HomeService
                {
                    Id = 175,
                    Name = "آموزش دروس دانشگاهی",
                    ImageUrl = "/images/HomeService/26-Amozesh/05.jpg",
                    Description = "دوره‌های تخصصی و دروس دانشگاهی برای دانشجویان.",
                    BasePrice = 2000000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 26
                },
                //************************************************************
                 new HomeService
                 {
                     Id = 176,
                     Name = "امداد و درمان",
                     ImageUrl = "/images/HomeService/27-KhadamatFori/05.jpg",
                     Description = "خدمات امداد و درمان فوری برای حوادث و بیماری‌های مختلف.",
                     BasePrice = 500000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 27
                 },
                new HomeService
                {
                    Id = 177,
                    Name = "باز کردن درب خودرو",
                    ImageUrl = "/images/HomeService/27-KhadamatFori/04.jpg",
                    Description = "خدمات باز کردن درب خودرو در مواقع اضطراری.",
                    BasePrice = 200000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 27
                },
                new HomeService
                {
                    Id = 178,
                    Name = "کلید سازی",
                    ImageUrl = "/images/HomeService/27-KhadamatFori/01.jpg",
                    Description = "خدمات ساخت کلید برای انواع درب‌ها و خودروها.",
                    BasePrice = 150000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 27
                },
                new HomeService
                {
                    Id = 179,
                    Name = "جک و ریموت",
                    ImageUrl = "/images/HomeService/27-KhadamatFori/02.jpg",
                    Description = "خدمات تعمیر و تعویض جک و ریموت خودرو.",
                    BasePrice = 300000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 27
                },
                new HomeService
                {
                    Id = 180,
                    Name = "رفع اتصالی و قطعی برق",
                    ImageUrl = "/images/HomeService/27-KhadamatFori/03.jpg",
                    Description = "خدمات رفع اتصالی و قطعی برق در منازل و خودروها.",
                    BasePrice = 250000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 27
                },
                //********************************************
                 new HomeService
                 {
                     Id = 181,
                     Name = "گوسفند زنده",
                     ImageUrl = "/images/HomeService/28-HameFanHarif/01.jpg",
                     Description = "خدمات فروش گوسفند زنده برای مصرف خوراکی و دیگر موارد.",
                     BasePrice = 5000000,
                     ViewCount = 0,
                     IsDeleted = false,
                     SubCategoryId = 28
                 },
                new HomeService
                {
                    Id = 182,
                    Name = "تایپ متون",
                    ImageUrl = "/images/HomeService/28-HameFanHarif/01.jpg",
                    Description = "خدمات تایپ متون و اسناد به صورت دقیق و سریع.",
                    BasePrice = 50000,
                    ViewCount = 0,
                    IsDeleted = false,
                    SubCategoryId = 28
                }
            );
        }
    }
}
