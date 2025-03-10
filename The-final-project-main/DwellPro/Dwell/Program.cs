using App.Domain.AppServices.Home.AppServices.Categories;
using App.Domain.AppServices.Home.AppServices.ListOrder;
using App.Domain.AppServices.Home.AppServices.ListOrder.Order;
using App.Domain.AppServices.Home.AppServices.Other;
using App.Domain.AppServices.Home.AppServices.Users;
using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.Contract.AppServices.ListOrder.Order;
using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Contract.Repositories.ListOrder;
using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Contract.Repositories.Users;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.Contract.Services.ListOrder;
using App.Domain.Core.Home.Contract.Services.ListOrder.Order;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Contract.Services.Users;
using App.Domain.Core.Home.Entities.Users;
using App.Domain.Services.Home.Services.Categories;
using App.Domain.Services.Home.Services.ListOrder;
using App.Domain.Services.Home.Services.ListOrder.Order;
using App.Domain.Services.Home.Services.Other;
using App.Domain.Services.Home.Services.Users;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using App.Infra.Data.Repos.Ef.Home.Repository.Categories;
using App.Infra.Data.Repos.Ef.Home.Repository.ListOrder;
using App.Infra.Data.Repos.Ef.Home.Repository.Other;
using App.Infra.Data.Repos.Ef.Home.Repository.Users;
using DwellMVC.BackgroundServices;
using DwellMVC.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

}).AddRoles<IdentityRole<int>>()
  .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<RandomHomeServicesUpdater>();

builder.Services.AddScoped<AppDbContext>();
//***********************************************************************************************
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();
builder.Services.AddScoped<IExpertProposalRepository, ExpertProposalRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IExpertHomeServiceRepository, ExpertHomeServiceRepository>();
builder.Services.AddScoped<IPicturesRepository, PicturesRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//***********************************************************************************************
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IHomeServiceService, HomeServiceService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IExpertProposalService, ExpertProposalService>();
builder.Services.AddScoped<IOrderManagementService, OrderManagementService>();
builder.Services.AddScoped<IViewOrderService, ViewOrderService>();

builder.Services.AddScoped<IAdminCommentService, AdminCommentService>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IPictureService, PictureService>();
builder.Services.AddScoped<IExpertHomeServiceService, ExpertHomeServiceService>();
//************************************************************************************************
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<IHomeServiceAppService, HomeServiceAppService>();
builder.Services.AddScoped<ISubCategoryAppService, SubCategoryAppService>();
builder.Services.AddScoped<IOrderManagementAppService, OrderManagementAppService>();
builder.Services.AddScoped<IViewOrderAppService, ViewOrderAppService>();
builder.Services.AddScoped<IExpertProposalAppService, ExpertProposalAppService>();
builder.Services.AddScoped<IOrderAppService, OrderAppService>();
builder.Services.AddScoped<IAdminCommentAppService, AdminCommentAppService>();
builder.Services.AddScoped<ICityAppService, CityAppService>();
builder.Services.AddScoped<IAdminUserAppService, AdminUserAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();
builder.Services.AddScoped<IPictureAppService, PictureAppService>();
builder.Services.AddScoped<IExpertHomeServiceAppService, ExpertHomeServiceAppService>();


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddSeq("http://localhost:5341", "7wYnJ2JllmtF4SX7p1vp");


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Admin/Login";
    options.AccessDeniedPath = "/Home/Index";

});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
