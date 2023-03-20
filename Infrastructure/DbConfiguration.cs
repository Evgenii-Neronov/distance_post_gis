using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MyApp.Infrastructure;

public static class DbConfiguration
{
    public static DbContextOptions CreateOptions(string host, string dbname)
    {
        var connectionString = CreateConnectionString(dbname, host);
        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.ReplaceService<IModelCustomizer, DefaultModelCustomizer>();
        return optionsBuilder.Options;
    }

    public static string CreateConnectionString(string host, string dbname, string username = "testuser",
        string password = "testpass")
    {
        return $"Host={host};Database={dbname};Username={username};Password={password}";
    }
}