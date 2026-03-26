using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver;
using upnews_admin_panel.Core.Application.Services.Auth;
using upnews_admin_panel.Core.Application.Services.Auth_Services;
using upnews_admin_panel.Core.Application.Services.MongoDB_Services;
using upnews_admin_panel.Core.Domain.Interfaces;
using upnews_admin_panel.Core.Domain.Interfaces.IAuth;
using upnews_admin_panel.Core.Infrastructure.Data.MongoDB;

var builder = WebApplication.CreateBuilder(args);


//configuracion de MongoDB
builder.Services.Configure<MongoDB_Settings>(
    builder.Configuration.GetSection("MongoDB_Settings"));

//registrar servicios 
builder.Services.AddScoped<IMongoDB_Service, MongoDB_Service>();
builder.Services.AddScoped<ILogin_Service, Login_Service>();
builder.Services.AddScoped<ICookieAuth_Service, CookieAuth_Service>();



//cookie based auth configuration
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "CookieAuth";
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });


builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Core/Web/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Core/Web/Views/Shared/{0}.cshtml");
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
