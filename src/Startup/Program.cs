using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetCheckup.Infrastructure;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(async (hostContext, services) =>
{
    var configuration = hostContext.Configuration;

    await services.AddInfrastructureServices(configuration);

});

var app = builder.Build();

await app.RunAsync();
