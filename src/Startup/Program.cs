using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetCheckup.Application;
using VetCheckup.Domain.Entities;
using VetCheckup.Infrastructure;
using VetCheckup.Infrastructure.Data;
using VetCheckup.Infrastructure.Data.Seeding;
using VetCheckup.Infrastructure.Identity;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    var configuration = hostContext.Configuration;

    services.AddApplicationServices();
    services.AddInfrastructureServices(configuration);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await IdentitySeeder.SeedRolesAsync(roleManager);

    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
    await DatabaseSeeder.SeedDevelopmentDataAsync(dbContext, passwordHasher);
}

await app.RunAsync();
