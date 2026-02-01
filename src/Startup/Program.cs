using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetCheckup.Domain.Entities;
using VetCheckup.Infrastructure;
using VetCheckup.Infrastructure.Data;
using VetCheckup.Infrastructure.Identity;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    var configuration = hostContext.Configuration;

    services.AddInfrastructureServices(configuration);

    services.AddIdentityCore<User>()
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
});

var app = builder.Build();  

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await IdentitySeeder.SeedRolesAsync(roleManager);
}

await app.RunAsync();
