using LearnASPWithAkichan.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<regist_courseContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
//CẤu hình Unicode
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
//Cookies
builder.Services.AddAuthentication("CookieAuth").AddCookie
    ("CookieAuth",
        options =>
        {
            options.Cookie.Name = "CookieAuth";
            options.LogoutPath = "/Accounts/Index";
            options.AccessDeniedPath = "/Home/Error";
        }
    );
//Authorize
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("Admin");
        });
    options.AddPolicy("Student",
      policy =>
      {
          policy.RequireAuthenticatedUser();
          policy.RequireClaim("Student");
      });

});
//Session
builder.Services.AddSession();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Index}/{id?}");

app.Run();
