using Microsoft.EntityFrameworkCore;
using WeatherArchive.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WeatherArchiveContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("WeatherArchiveDb")));

builder.Services.AddScoped<IWeatherReportRepository, WeatherReportRepository>();

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
