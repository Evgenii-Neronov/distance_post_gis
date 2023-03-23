using Domain;
using MyApp.Infrastructure;
using NetTopologySuite.Geometries;

namespace test_data_generator;

internal class Program
{
    private static readonly Random _random = new Random();

    public static double GetRandLat() => (double)_random.Next(-70, 70);
    public static double GetRandLong() => _random.Next(-120, 120);

    static void Main(string[] args)
    {
        var contextType = typeof(MyDbContext);
        var myDbContext = (MyDbContext)Activator.CreateInstance(contextType, DbConfiguration.CreateOptions("127.0.0.1", "my_postgis"));

        for (int j = 0; j < 300; j++)
        {
            for (int i = 0; i < 1000; i++)
            {
                var fA = myDbContext.Set<AFacilityA>();
                var lat = GetRandLat();
                var lon = GetRandLong();
                var id = Guid.NewGuid();
                fA.Add(new AFacilityA()
                {
                    Id = id,
                    Latitude = lat,
                    Longitude = lon,
                    Name = $"facility_{lat}_{lon}",
                    Location = new Point(lon, lat),
                });

                var fB = myDbContext.Set<BFacilityB>();
                fB.Add(new BFacilityB()
                {
                    Id = id,
                    Latitude = lat,
                    Longitude = lon,
                    Name = $"facility_{lat}_{lon}",
                });

                var fC = myDbContext.Set<CFacilityC>();
                fC.Add(new CFacilityC()
                {
                    Id = id,
                    Latitude = lat,
                    Longitude = lon,
                    Name = $"facility_{lat}_{lon}",
                });
            }

            myDbContext.SaveChanges();

            Console.WriteLine($"iteration - {j}");
        }


        Console.WriteLine("Hello, World!");
    }
}
