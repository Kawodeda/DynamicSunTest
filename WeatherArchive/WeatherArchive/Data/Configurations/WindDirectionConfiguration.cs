using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherArchive.Models;

namespace WeatherArchive.Data.Configurations
{
    public class WindDirectionConfiguration : IEntityTypeConfiguration<WindDirection>
    {
        public void Configure(EntityTypeBuilder<WindDirection> builder)
        {
            builder.ToTable(nameof(WindDirection));

            builder.Property(x => x.Id)
                .IsRequired();
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(x => x.Id);
        }
    }
}