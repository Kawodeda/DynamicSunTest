using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherArchive.Models;

namespace WeatherArchive.Data.Configurations
{
    public class WeatherReportConfiguration : IEntityTypeConfiguration<WeatherReport>
    {
        public void Configure(EntityTypeBuilder<WeatherReport> builder)
        {
            builder.ToTable(nameof(WeatherReport));

            builder.Property(x => x.Id)
                .IsRequired();
            builder.Property(x => x.Timestamp)
                .IsRequired()
                .HasColumnType("timestamp");
            builder.Property(x => x.Temperature)
                .IsRequired();
            builder.Property(x => x.Humidity)
                .IsRequired();
            builder.Property(x => x.DewPoint)
                .IsRequired();
            builder.Property(x => x.Pressure)
                .IsRequired();
            builder.Property(x => x.WindSpeed)
                .IsRequired();
            builder.Property(x => x.Cloudiness);
            builder.Property(x => x.CloudBase)
                .IsRequired();
            builder.Property(x => x.HorizontalVisibility);

            builder
                .HasMany(x => x.WindDirections)
                .WithMany()
                .UsingEntity(
                    l => l.HasOne(typeof(WindDirection)).WithMany().HasForeignKey("WindDirection"),
                    r => r.HasOne(typeof(WeatherReport)).WithMany().HasForeignKey("WeatherReport"));

            builder
                .HasMany(x => x.WeatherPhenomena)
                .WithMany()
                .UsingEntity(
                    l => l.HasOne(typeof(WeatherPhenomenon)).WithMany().HasForeignKey("WeatherPhenomenon"),
                    r => r.HasOne(typeof(WeatherReport)).WithMany().HasForeignKey("WeatherReport"));

            builder.HasKey(x => x.Id);
        }
    }
}