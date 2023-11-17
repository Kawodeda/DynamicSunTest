using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherArchive.Models;

namespace WeatherArchive.Data.Configurations
{
    public class WeatherPhenomenonConfiguration : IEntityTypeConfiguration<WeatherPhenomenon>
    {
        public void Configure(EntityTypeBuilder<WeatherPhenomenon> builder)
        {
            builder.ToTable(nameof(WeatherPhenomenon));

            builder.Property(x => x.Id)
                .IsRequired();
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(x => x.Id);
        }
    }
}