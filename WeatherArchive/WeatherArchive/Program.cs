using Microsoft.EntityFrameworkCore;
using WeatherArchive.Data;
using WeatherArchive.Data.Parsing;
using WeatherArchive.Data.Repositories;
using WeatherArchive.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WeatherArchiveContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("WeatherArchiveDb")));

builder.Services.AddScoped<IWeatherReportRepository, WeatherReportRepository>();
builder.Services.AddScoped<IWeatherReportExcelParser, WeatherReportExcelParser>();
builder.Services.AddScoped<IWeatherArchiveUploadService, WeatherArchiveUploadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
