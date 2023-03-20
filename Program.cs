using MyApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MyDbContext>(serviceProvider =>
{
    var contextType = typeof(MyDbContext);
    var context = Activator.CreateInstance(contextType, new[] { DbConfiguration.CreateOptions("locahost", "my_postgis") });
    return (MyDbContext)context;
});
        
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
