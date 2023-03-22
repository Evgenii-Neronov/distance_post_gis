using Microsoft.EntityFrameworkCore;

namespace MyApp.Infrastructure;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        //modelBuilder.ApplyConfiguration(new FacilityConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityAConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityBConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityCConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}