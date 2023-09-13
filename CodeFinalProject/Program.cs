using CodeFinalProject.DAL;
using CodeFinalProject.Email;
using CodeFinalProject.Models;
using CodeFinalProject.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
    .AddFluentValidation(p=>p.RegisterValidatorsFromAssemblyContaining<BookOnlineVMValidator>());


builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;
}).AddDefaultTokenProviders()
.AddEntityFrameworkStores<HimaraDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToAccessDenied = options.Events.OnRedirectToLogin = context =>
    {
        var uri = new Uri(context.RedirectUri);
        if (context.HttpContext.Request.Path.Value.StartsWith("/manage"))
        {
            context.Response.Redirect("/manage/account/login" + uri.Query);
        }
        else
        {
            context.Response.Redirect("/account/login" + uri.Query);
        }
        return Task.CompletedTask;
    };
});

builder.Services.AddDbContext<HimaraDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();
