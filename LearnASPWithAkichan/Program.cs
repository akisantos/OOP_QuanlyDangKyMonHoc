using LearnASPWithAkichan.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Cấu hình dịch vụ RazorRuntimeCompilation.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Cấu hình kết nối cơ sở dữ liệu.
builder.Services.AddDbContext<registrion_course2Context>(options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DbContext").Value));
//CẤu hình Unicode
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
// Cấu hình cơ chế xác thực Cookie. Có thể đặt tên co Cookie("tên ở đây nè"); tên mặc định sẽ là cookies
builder.Services.AddAuthentication("CookieAuth").AddCookie
    ("CookieAuth",
        options =>
        {
            options.Cookie.Name = "CookieAuth";
            options.LogoutPath = "/Login/SignIn";
            options.AccessDeniedPath = "/Login/AccessDenied";
        }
    );

builder.Services.AddSession();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Xác thực
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");

app.Run();
