using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;
using MoneyPlus.Services;
using MoneyPlus.Services.EmailService;
using MoneyPlus.Services.NewsService;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddRazorPages();


builder.Services.AddScoped<Account>();
builder.Services.AddScoped<Transfer>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<EmailBackgroundService>();
builder.Services.AddScoped<INewsService, NewsService>();    


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

var defaultCultrure = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCultrure),
    SupportedCultures = new List<CultureInfo> { defaultCultrure },
    SupportedUICultures = new List<CultureInfo> { defaultCultrure }
};
app.UseRequestLocalization(localizationOptions);
