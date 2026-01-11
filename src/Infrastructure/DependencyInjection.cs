using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;
using VetCheckup.Infrastructure.Data;

namespace VetCheckup.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // The fallback of ConnectionStrings.DefaultConnection matches the JSON section in the consumers appsettings.json. If the file does not have it, this will be null.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? configuration.GetSection("ConnectionStrings")["DefaultConnection"];

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddSingleton(TimeProvider.System);
        services.AddDataProtection();

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        using var _ServiceProvider = services.BuildServiceProvider();
        {
            var _DbContext = _ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _DbContext.Database.Migrate();
        }

        return services;
    }
}
