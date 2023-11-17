using Microsoft.EntityFrameworkCore;
using WeatherArchive.Data.Configurations;
using WeatherArchive.Models;

namespace WeatherArchive.Data
{
    public class WeatherArchiveContext : DbContext
    {
        public WeatherArchiveContext(DbContextOptions<WeatherArchiveContext> options) : base(options)
        {

        }

        public DbSet<WindDirection> WindDirections { get; set; }

        public DbSet<WeatherPhenomenon> WeatherPhenomena { get; set; }

        public DbSet<WeatherReport> WeatherReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new WindDirectionConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherPhenomenonConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherReportConfiguration());
        }
    }
}