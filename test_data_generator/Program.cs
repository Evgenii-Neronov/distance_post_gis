using Domain;
using MyApp.Infrastructure;

namespace test_data_generator;

internal class Program
{
    static void Main(string[] args)
    {
        var contextType = typeof(MyDbContext);
        var myDbContext = (MyDbContext)Activator.CreateInstance(contextType, DbConfiguration.CreateOptions("127.0.0.1", "my_postgis"));

        var userSet = myDbContext.Set<User>();

        userSet.Add(new User()
        {
            Id = Guid.NewGuid(),
            Latitude = 4,
            Longitude = 3,
            Name = "ConsoleUser"
        });

        myDbContext.SaveChanges();

        Console.WriteLine("Hello, World!");
    }
}
