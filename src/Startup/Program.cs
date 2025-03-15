using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetCheckup.Infrastructure;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    var configuration = hostContext.Configuration;

    services.AddInfrastructureServices(configuration);

});

var app = builder.Build();

await app.RunAsync();
