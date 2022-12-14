using MidasShopSolution.CustomerSite.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentValidation.AspNetCore;
using MidasShopSolution.ViewModels.Users;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>())
     .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    });
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Users/Login/";
    options.AccessDeniedPath = "/Account/Forbidden/";
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddTransient<IUserApiClient, UserApiClient>();
// builder.Services.AddTransient<ICommentApiClient, CommentApiClient>();

// builder.Services.AddScoped<ICommentApiClient, CommentApiClient>();

builder.Services
    .AddRefitClient<IProductApiClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001/"));

builder.Services
    .AddRefitClient<ICommentApiClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001/"));

builder.Services
    .AddRefitClient<ICategoryApiClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001/"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();