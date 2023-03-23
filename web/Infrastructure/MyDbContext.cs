using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NpgsqlTypes;

namespace MyApp.Infrastructure;

public class MyDbContext : DbContext
{
    [DbFunction("earth_distance", "public")]
    public static double GetDistance(NpgsqlPoint p1, NpgsqlPoint p2) 
    {
        throw new NotSupportedException();
    }

    [DbFunction("abc123", "public")]
    public static int Abc123(int a)
    {
        throw new NotSupportedException();
    }

    /*
    public int GetDistance(double lat1, double lon1, double lat2, double lon2)
    {
        return Database.ExecuteSqlRaw($"select public.earth_distance(ll_to_earth({lon1},{lat1}), ll_to_earth({lon2},{lat2}))");
    }*/

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder.ApplyConfiguration(new FacilityConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityAConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityBConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityCConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        //modelBuilder.HasDbFunction(() => GetDistance(default(long), default(long), default(long), default(long)));

    }
}