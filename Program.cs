using Blogly.Data;
using Blogly.Helpers;
using Blogly.Models;
using Blogly.Services;
using Blogly.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<BlogUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//Register custom DataService Class
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<BlogSearchService>();

//Register a preconfigured instance of MailSettings class
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IBlogEmailSender, EmailService>();

//Register Image Service
builder.Services.AddScoped<IImageService, BasicImageService>();

//Register the Slug Service
builder.Services.AddScoped<ISlugService, BasicSlugService>();

var app = builder.Build();

var scope = app.Services.CreateScope();
await DataHelper.ManageDataAsync(scope.ServiceProvider);

//pull out registered DataService
var dataService = app.Services
                     .CreateScope().ServiceProvider
                     .GetRequiredService<DataService>();

await dataService.ManageDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Custom map controller route
app.MapControllerRoute(
    name: "SlugRoute",
    pattern: "BlogPosts/UrlFriendly/{slug}",
    defaults: new { controller = "Posts", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
