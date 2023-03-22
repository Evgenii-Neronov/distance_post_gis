using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MyApp.Infrastructure;

public static class DbConfiguration
{
    public static DbContextOptions CreateOptions(string host, string dbname)
    {
        var connectionString = CreateConnectionString(host, dbname);
        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder.UseNpgsql(connectionString, x => x.UseNetTopologySuite());
        optionsBuilder.ReplaceService<IModelCustomizer, DefaultModelCustomizer>();
        
        return optionsBuilder.Options;
    }

    public static string CreateConnectionString(string host, string dbname, string username = "testuser",
        string password = "testpass")
    {
        return $"Host={host};Port={15432};Database={dbname};Username={username};Password={password}";
    }
}