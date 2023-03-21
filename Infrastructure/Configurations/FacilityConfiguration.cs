using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyApp.Infrastructure;

public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
{
    void IEntityTypeConfiguration<Facility>.Configure(EntityTypeBuilder<Facility> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.Latitude);
        builder.Property(x => x.Longitude);
        builder.Property(b => b.Location).HasColumnType("geography (point)");
    }
}