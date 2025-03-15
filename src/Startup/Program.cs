using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetCheckup.Infrastructure;

var builder = Host.CreateDefaultBuilder(args);

// Add services to the container.
builder.ConfigureServices((hostContext, services) =>
{
    // This will load configuration from appsettings.json and other sources (like environment variables).
    var configuration = hostContext.Configuration;

    // Call the method to add infrastructure services
    services.AddInfrastructureServices(configuration);

    // You can add other services or configuration here if necessary
});

var app = builder.Build();

// Run the application (this is for console apps, adjust for web apps if needed)
await app.RunAsync();
