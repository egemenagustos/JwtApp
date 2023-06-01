using FluentValidation;
using JwtApp.Front.Models;
using JwtApp.Front.ValidationRules;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllersWithViews();
services.AddHttpClient();

services.AddTransient<IValidator<CategoryUpdateModel>,CategoryUpdateValidator>();
services.AddTransient<IValidator<CreateCategoryModel>, CategoryCreateValidator>();

services.AddTransient<IValidator<CreateProductModel>, ProductCreateValidator>();
services.AddTransient<IValidator<UpdateProductModel>, ProductUpdateValidator>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/LogOut";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    opt.Cookie.Name = "JwtCookie";
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
