using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(serviceProvider =>
{
    var contextType = typeof(MyDbContext);
    var context = Activator.CreateInstance(contextType, DbConfiguration.CreateOptions("locahost", "my_postgis"));
    return (MyDbContext)context;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
context.Database.Migrate();

app.Run();